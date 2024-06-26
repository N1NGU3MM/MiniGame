using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float velocidade = 4; // Velocidade que o personagem ir� se mover

    private Vector2 myInput; // Vector2 que armazena os inputs do joystick de movimento
    private CharacterController characterController; // Refer�ncia ao componente de CharacterController do personagem
    private Transform myCamera; // Refer�ncia a c�mera principal da cena
    private Animator animator; // Refer�ncia ao componente Animator do personagem

    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); // Nesse c�digo � feito a referencia��o
        animator = GetComponent<Animator>(); // Nesse c�digo � feito a referencia��o
        myCamera = Camera.main.transform; // Nesse c�digo � feito a referencia��o
    }

    /// <summary>
    /// Metodo respons�vel por obter as entradas do joystick.
    /// </summary>
    /// <param name="value">Callback com as entradas de joystick, vindos do Inputs Actions</param>
    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    
    private void Update()
    {
        RotacionarPersonagem(); // Chama o m�todo para definir a rota��o do personagem
        characterController.Move(transform.forward * myInput.magnitude * velocidade * Time.deltaTime);        
        characterController.Move(Vector3.down * 9.81f * Time.deltaTime);

        animator.SetBool("andar", myInput != Vector2.zero);
    }
    
    /// <summary>
    /// Rotaciona o personagem de acordo com a posi��o da c�mera e entradas do usu�rio
    /// </summary>
    private void RotacionarPersonagem()
    {
        Vector3 forward = myCamera.TransformDirection(Vector3.forward); // Armazena um vetor que indica a dire��o "para frente"
        
        Vector3 right = myCamera.TransformDirection(Vector3.right); // Armazena um vetor que indica a dire��o "para o lado direito"


        /* Faz um calculo para indicar a dire��o que o personagem deve seguir, levando em considera��o as entradas no joystick e a posi��o da c�mera. 
           Por exemplo, se quisermos ir para frente, nossa dire��o n�o � X=0, Y=0, Z=1, pois essa dire��o est� no eixo global, se nossa c�mera estiver
           virada para a esquerda, ir para frente signica ir na dire��o X=-1, Y=0, Z=0. Por isso, a linha abaixo basicamente faz essa convers�o de dire��o,
           indicando um Vector de dire��o baseada na rota��o atual da c�mera.
        */
        Vector3 targetDirection = myInput.x * right + myInput.y * forward; 


        if (myInput != Vector2.zero && targetDirection.magnitude > 0.1f) // Verifica se o Input � diferente de 0 e se a magnitude(intesidade) do input � maior do que 0.1, em uma escala de 0 a 1. Ou seja, desconsidera pequenos movimentos no joystick.
        {
            Quaternion freeRotation = Quaternion.LookRotation(targetDirection.normalized); // Cria uma rota��o com as dire��es forward. Ou seja, retorna uma rota��o indicando a dire��o alvo.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), 10 * Time.deltaTime); // Aplica a rota��o ao personagem. O m�todo Quaternion.Slerp aplica uma suaviza��o na rota��o, para que ela n�o aconte�a de forma abrupta
        }
    }

}
