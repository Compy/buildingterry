/*==============================================================================
Copyright (c) 2010-2013 QUALCOMM Austria Research Center GmbH.
All Rights Reserved.
==============================================================================*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

/// <summary>
/// Menu that appears on double tap, enables and disables the AutoFocus on the camera.
/// </summary>
public class VirtualButtonsMenu : MonoBehaviour, ITrackerEventHandler
{
    #region PUBLIC_MEMBER_VARIABLES

    // Material for outlining the virtual button.
    public Material m_VirtualButtonMaterial;

    #endregion // PUBLIC_MEMBER_VARIABLES



    #region PRIVATE_MEMBER_VARIABLES

    // Check if a menu button has been pressed.
    private bool mButtonPressed = false;

    // If the menu is currently open
    private bool mMenuOpen = false;

    // Contains if the device supports continous autofocus
    private bool mContinousAFSupported = true;

    // Contains the currently set auto focus mode.
    private CameraDevice.FocusMode mFocusMode =
        CameraDevice.FocusMode.FOCUS_MODE_NORMAL;

    // Contains the rectangle for the camera options menu.
    private Rect mAreaRect;

    // this is used to distinguish single and double taps
    private bool mWaitingForSecondTap;
    private Vector3 mFirstTapPosition;
    private DateTime mFirstTapTime;
    // the maximum distance that is allowed between two taps to make them count as a double tap
    // (relative to the screen size)
    private const float MAX_TAP_DISTANCE_SCREEN_SPACE = 0.1f;
    private const int MAX_TAP_MILLISEC = 500;

    // Wood image target.
    private ImageTargetBehaviour mImageTargetWood = null;

    // Dictionary for storing virtual button positions.
    private Dictionary<string, Vector3> mVBPositionDict =
        new Dictionary<string, Vector3>();

    // Dictionary for storing virtual button scale values.
    private Dictionary<string, Vector3> mVBScaleDict =
        new Dictionary<string, Vector3>();

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // register for the OnInitialized event at the QCARBehaviour
        QCARBehaviour qcarBehaviour = (QCARBehaviour)FindObjectOfType(typeof(QCARBehaviour));
        if (qcarBehaviour)
        {
            qcarBehaviour.RegisterTrackerEventHandler(this);
        }

        // Setup position and size of the camera menu.
        ComputePosition();

        // Find the Wood image target.
        mImageTargetWood = GameObject.Find("ImageTargetWood").GetComponent<ImageTargetBehaviour>();

        // Add a mesh for each virtual button on the Wood target.
        VirtualButtonBehaviour[] vbs =
                mImageTargetWood.gameObject.GetComponentsInChildren<VirtualButtonBehaviour>();
        foreach (VirtualButtonBehaviour vb in vbs)
        {
            CreateVBMesh(vb);
            // Also store the position and scale for later.
            mVBPositionDict.Add(vb.VirtualButtonName, vb.transform.localPosition);
            mVBScaleDict.Add(vb.VirtualButtonName, vb.transform.localScale);
        }
    }


    void Update()
    {
        // If the touch event results from a button press it is ignored.
        if (!mButtonPressed)
        {
            if (mMenuOpen)
            {
                // If finger is removed from screen.
                if (Input.GetMouseButtonUp(0))
                {
                    HandleSingleTap();
                }
            }
            else
            {
                // check if it is a doulbe tap
                if (Input.GetMouseButtonUp(0))
                {
                    if (mWaitingForSecondTap)
                    {
                        // check if time and position match:
                        int smallerScreenDimension = Screen.width < Screen.height ? Screen.width : Screen.height;
                        if (DateTime.Now - mFirstTapTime < TimeSpan.FromMilliseconds(MAX_TAP_MILLISEC) &&
                            Vector4.Distance(Input.mousePosition, mFirstTapPosition) < smallerScreenDimension * MAX_TAP_DISTANCE_SCREEN_SPACE)
                        {
                            // it's a double tap
                            HandleDoubleTap();
                        }
                        else
                        {
                            // too late/far to be a double tap, treat it as first tap:
                            mFirstTapPosition = Input.mousePosition;
                            mFirstTapTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        // it's the first tap
                        mWaitingForSecondTap = true;
                        mFirstTapPosition = Input.mousePosition;
                        mFirstTapTime = DateTime.Now;
                    }
                }
                else
                {
                    // time window for second tap has passed, trigger single tap
                    if (mWaitingForSecondTap && DateTime.Now - mFirstTapTime > TimeSpan.FromMilliseconds(MAX_TAP_MILLISEC))
                    {
                        HandleSingleTap();
                    }
                }
            }
        }
        else
        {
            mButtonPressed = false;
        }
    }


    void OnGUI()
    {
        if (mMenuOpen)
        {
            ComputePosition();

            // Setup style for buttons.
            GUIStyle buttonGroupStyle = new GUIStyle(GUI.skin.button);
            buttonGroupStyle.stretchWidth = true;
            buttonGroupStyle.stretchHeight = true;

            GUILayout.BeginArea(mAreaRect);

            GUILayout.BeginVertical(buttonGroupStyle);

            GUILayout.BeginHorizontal(buttonGroupStyle);

            // Create and destroy virtual buttons.
            if (GUILayout.Button("Toggle Red", buttonGroupStyle))
            {
                ToggleVirtualButton("red");
            }
            if (GUILayout.Button("Toggle Blue", buttonGroupStyle))
            {
                ToggleVirtualButton("blue");
            }
            if (GUILayout.Button("Toggle Yellow", buttonGroupStyle))
            {
                ToggleVirtualButton("yellow");
            }
            if (GUILayout.Button("Toggle Green", buttonGroupStyle))
            {
                ToggleVirtualButton("green");
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(buttonGroupStyle);

            if (!mContinousAFSupported)
            {
                if (GUILayout.Button("Cont. Auto Focus not supported on this device", buttonGroupStyle))
                {
                    mMenuOpen = false;
                    mButtonPressed = true;
                }
            }
            else
            {
                // toggle continuous autofocus
                if (mFocusMode == CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO)
                {
                    // button to change to single auto focus:
                    if (GUILayout.Button("Deactivate Cont. Auto Focus", buttonGroupStyle))
                    {
                        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL))
                            mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_NORMAL;

                        mMenuOpen = false;
                        mButtonPressed = true;
                    }
                }
                else
                {
                    // button to change to cont. auto focus:
                    if (GUILayout.Button("Activate Cont. Auto Focus", buttonGroupStyle))
                    {
                        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
                            mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;

                        mMenuOpen = false;
                        mButtonPressed = true;
                    }
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region ITrackerEventHandler_IMPLEMENTATION

    /// <summary>
    /// This method is called when QCAR has finished initializing
    /// </summary>
    public void OnInitialized()
    {
        // try to set continous auto focus as default
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
        }
        else
        {
            Debug.LogError("could not switch to continuous autofocus");
            mContinousAFSupported = false;
        }
    }

    public void OnTrackablesUpdated()
    {
        // not used
    }

    #endregion //ITrackerEventHandler_IMPLEMENTATION



    #region PRIVATE_METHODS

    private void HandleSingleTap()
    {
        mWaitingForSecondTap = false;

        if (mMenuOpen)
            mMenuOpen = false;
        else
        {
            // trigger focus once
            if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO))
                mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO;
        }
    }

    private void HandleDoubleTap()
    {
        mWaitingForSecondTap = false;
        mMenuOpen = true;
    }

    /// <summary>
    /// Compute the coordinates of the menu depending on the current orientation.
    /// </summary>
    private void ComputePosition()
    {
        int areaWidth = Screen.width;
        int areaHeight = (Screen.height / 5) * 2;
        int areaLeft = 0;
        int areaTop = Screen.height - areaHeight;
        mAreaRect = new Rect(areaLeft, areaTop, areaWidth, areaHeight);
    }


    /// <summary>
    /// Create a mesh outline for the virtual button.
    /// </summary>
    private void CreateVBMesh(VirtualButtonBehaviour vb)
    {
        GameObject vbObject = vb.gameObject;

        MeshFilter meshFilter = vbObject.GetComponent<MeshFilter>();
        if (!meshFilter)
        {
            meshFilter = vbObject.AddComponent<MeshFilter>();
        }

        // Setup vertex positions.
        Vector3 p0 = new Vector3(-0.5f, 0, -0.5f);
        Vector3 p1 = new Vector3(-0.5f, 0, 0.5f);
        Vector3 p2 = new Vector3(0.5f, 0, -0.5f);
        Vector3 p3 = new Vector3(0.5f, 0, 0.5f);

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
        mesh.triangles = new int[]  {
                                        0,1,2,
                                        2,1,3
                                    };

        // Add UV coordinates.
        mesh.uv = new Vector2[]{
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0,1),
                new Vector2(1,1)
                };

        // Add empty normals array.
        mesh.normals = new Vector3[mesh.vertices.Length];

        // Automatically calculate normals.
        mesh.RecalculateNormals();
        mesh.name = "VBPlane";

        meshFilter.sharedMesh = mesh;

        MeshRenderer meshRenderer = vbObject.GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = vbObject.AddComponent<MeshRenderer>();
        }

        meshRenderer.sharedMaterial = m_VirtualButtonMaterial;
    }


    /// <summary>
    /// Create or destroy the virtual button with the given name.
    /// </summary>
    private void ToggleVirtualButton(string name)
    {
        if (mImageTargetWood.ImageTarget != null)
        {
            // Get the virtual button if it exists.
            VirtualButton vb = mImageTargetWood.ImageTarget.GetVirtualButtonByName(name);

            if (vb != null)
            {
                // Destroy the virtual button if it exists.
                mImageTargetWood.DestroyVirtualButton(name);
            }
            else
            {
                // Get the position and scale originally used for this virtual button.
                Vector3 position, scale;
                if (mVBPositionDict.TryGetValue(name, out position) &&
                    mVBScaleDict.TryGetValue(name, out scale))
                {
                    // Deactivate the dataset before creating the virtual button.
                    ImageTracker imageTracker = (ImageTracker)
                                                TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER);
                    DataSet dataSet = imageTracker.GetActiveDataSets().First();
                    imageTracker.DeactivateDataSet(dataSet);

                    // Create the virtual button.
                    VirtualButtonBehaviour vbb = mImageTargetWood.CreateVirtualButton(name,
                                                                                      new Vector2(position.x, position.z),
                                                                                      new Vector2(scale.x, scale.z));

                    if (vbb != null)
                    {
                        // Register the button with the event handler on the Wood target.
                        vbb.RegisterEventHandler(mImageTargetWood.GetComponent<VirtualButtonEventHandler>());

                        // Add a mesh to outline the button.
                        CreateVBMesh(vbb);
                    }

                    // If the Wood target isn't currently tracked hide the button.
                    if (mImageTargetWood.CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
                    {
                        vbb.GetComponent<Renderer>().enabled = false;
                    }

                    // Reactivate the dataset.
                    imageTracker.ActivateDataSet(dataSet);
                }
            }
        }
    }

    #endregion // PRIVATE_METHODS
}

