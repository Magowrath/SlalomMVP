using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    /*===============================================================================================================================
    =================================================================================================================================
                Vehicle Controller Prototype for Slalom V0.1

                To Do:
                    Core Functionality:
                        Acceleration
                        Breaking
                        Drag & Downforce Simulation
                        Cornering 
                        
    =================================================================================================================================
    ===============================================================================================================================*/
    Rigidbody myRigidbody;
    private InputAction throttleAction;


    //======================
    // Engine Variables
    //======================
    //public int MaxRPM = 3000;
    //public float DragFactor;



    

    //===================================================================================================================================
    //      Working Variables
    //===================================================================================================================================
    
    private float ForceLong; //Describes the forces acting along the forward axis of the vehicle
    private float ForceLat; // Describes the forces acting along the direction perpendicular to the froward axis and parrallel to the ground
    private float ForceDown; //Describes the forces acting downwards on the vehicle (Down Force + Gravity)

    private Vector3 FwdVector;

    private float ForwardSpeed;
    private float Traction;
    private float RollingResistance;
    private float RollingResistanceCoef = 12.8f;
    private float FwdDrag;
    private float DragCoef = 0.4257f;
    private float EngineBreaking;        //The Force that slows down the engin
    private float EngineRPMDecay = 150; //How Fast RPMs Decay
    private float EngineForce;
    private float EngineRPM;    //The Current RPM of the engine
    private float EngineRPMGain = 300;  //How Fast the engine gains RPMs
    private float GearingRatio = 0.1f;
    private Vector3 VectorForce; //The summed forces acting on the vehicle


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();


        throttleAction = InputSystem.actions.FindAction("throttle");
    }

    void Update()
    {
        
        //Updates to referance vehicle attributes

        ForwardSpeed = myRigidbody.linearVelocity.magnitude;
        FwdVector = transform.forward.normalized;

        //EngineSim

        if (throttleAction.IsPressed()) {
            EngineForce = 1000;
        }
        else
        {
            EngineForce = 0;
        }

        //External Forces

        FwdDrag = -(DragCoef * ForwardSpeed);

        RollingResistance = -(RollingResistanceCoef * ForwardSpeed);

        //Summed Forces
        
        ForceLong = (EngineForce + RollingResistance + FwdDrag);

        VectorForce = (FwdVector * ForceLong);

        Debug.Log(VectorForce);

        //Forces are applyed to the vehicle object

        myRigidbody.AddForce(VectorForce, ForceMode.Force);
    }
}
