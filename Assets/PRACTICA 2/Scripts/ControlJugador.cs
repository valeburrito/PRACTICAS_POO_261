using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // 1. Movimiento

    public float velocidad = 5f;
    public float gravedad = -9.8f;

    private CharacterController controller;
    private Vector3 velocidadVertical;

    // 2. Variables Vista
    //Transform que cámara verá como los ojos del jugador
    //Sensibilidad del mouse que tan rápido girara el mouse
    //Rotación x vertical cuántos grados va a poder ver hacia arriba o abajo el jugador 

    public Transform camara;
    public float sensibilidadMouse = 200f;

    private float rotacionXVertical = 0f;

    //Invocación

    private void Start()
    {
        controller = GetComponent<CharacterController>(); // Funciona para buscar la pieza de lego o componente 

        //Esta línea bloquea el puntero del mouse en los límites 

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ManejadorVista();
        ManejadorMovimiento();
    }

    void ManejadorVista()
    {
        // 1. Leer el imput del mouse 
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime; //
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime; //

        //2. Construir la rotación horizontal
        transform.Rotate(Vector3.up * mouseX);

        //3. Registro de la rotación vertical
        rotacionXVertical -= mouseY;

        //4. limitar la rotacion vertical
        Mathf.Clamp(rotacionXVertical, -90f, 90f);

        //5. Aplicar la rotacion
        // son los ejes          X          Y  Z
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);
    }

    void ManejadorMovimiento()
    {
        //1 leer el imput de movimiento (WASD o las flechas de dirección
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //2 Crear el vector de movimiento
        //se almacena de forma local el registro de direccion de movimiento 
        Vector3 direccion = transform.right * inputX + transform.forward * inputY;

        //3 Mover el CharaterController
        controller.Move(direccion * velocidad * Time.deltaTime);

        //4 Aplicar la gravedad
        //Registro si estoy en el piso para un futuro comportamiento de salto 
        if (controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;//Una pequeña fuerza hacia abajo para mantenerlo pegado al piso
        }

        //Aplicamos la aceleracion de la gravedad 
        velocidadVertical.y += gravedad * Time.deltaTime;

        //Movemos el controlador hacia abajo
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}