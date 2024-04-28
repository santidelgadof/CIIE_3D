using UnityEngine;
using UnityEngine.AI;

public class MouseAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject bolsaPrefab;
    private bool isMouseStopped = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameObject.SetActive(false); // Desactivar el ratón al principio

        // Llamar a la función para activar el ratón después de un tiempo aleatorio entre 0 y 60 segundos
        Invoke("ActivateMouse", Random.Range(0f, 20f));
    }

    void ActivateMouse()
    {
        gameObject.SetActive(true); // Activar el ratón después de un tiempo aleatorio
        SetRandomDestination(); // Establecer un destino aleatorio una vez que el ratón esté activo
    }


    void Update()
    {
        if (isMouseStopped == false)
        {

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                SetRandomDestination();
            }

            // Orientar el ratón hacia su destino
            if (navMeshAgent.velocity != Vector3.zero)
            {
                Vector3 lookDirection = navMeshAgent.velocity.normalized;
                lookDirection.y = 0f; // Para evitar que el ratón se incline hacia arriba o hacia abajo
                transform.rotation = Quaternion.LookRotation(-lookDirection);
            }
        }   
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + randomPoint, out hit, 10f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }
    

    // Método para detener o reanudar el movimiento del ratón
    public void SetMouseStopped(bool stopped)
    {
        isMouseStopped = stopped;
        navMeshAgent.isStopped = stopped;

        if (stopped)
        {
            TransformIntoBag();
        }
    }

    public void DestroyMouse()
    {
        isMouseStopped = true;
        navMeshAgent.isStopped = true;
        Destroy(gameObject);
    }


    public void TransformIntoBag()
    {
        GameObject bolsa = Instantiate(bolsaPrefab, transform.position + Vector3.up * 0.15f, Quaternion.Euler(-75f, 0f, 0f));

        Destroy(gameObject); // Destruir el ratón

        // Obtener el Rigidbody y el Collider de la bolsa
        Rigidbody bolsaRigidbody = bolsa.GetComponent<Rigidbody>();
        Collider bolsaCollider = bolsa.GetComponent<Collider>();

        // Si no hay un Rigidbody, añadir uno
        if (bolsaRigidbody == null)
        {
            bolsaRigidbody = bolsa.AddComponent<Rigidbody>();
        }

        // Si no hay un Collider, añadir uno
        if (bolsaCollider == null)
        {
            bolsaCollider = bolsa.AddComponent<BoxCollider>();
        }

        // Asignar la etiqueta "bag" al objeto de la bolsa
        bolsa.tag = "bag";
        bolsa.layer = LayerMask.NameToLayer("grab");

        // Habilitar el Rigidbody y el Collider
        bolsaRigidbody.isKinematic = false;
        bolsaCollider.enabled = true;


        // Ajustar la escala de la bolsa
        bolsa.transform.localScale = new Vector3(450f, 450f, 450f);
    }


    // Método para verificar si el ratón está detenido
    public bool IsMouseStopped()
    {
        return isMouseStopped;
    }
}
