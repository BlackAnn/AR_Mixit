using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


	public class InteractionControl : MonoBehaviour
	{
		[SerializeField] private MixingController mixingController;
	    [SerializeField] private GameManager gameManager;
	    

	private bool activated = false;
        private bool isInteracting;
		private ColorNames lastHitSphere;
		private float timeForInteraction = 1.0f;
		private float timer = 0.0f;
		private int swipeCount = 0;



        void Update()
        {
			/*if (activated)
			{
				if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit))
					{
						if (hit.collider != null)
						{
							Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
							hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
						}
					}
				}
			}*/

		if (activated)
		{
			if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
					{

						lastHitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
						isInteracting = true;
						timer = 0.0f;
						swipeCount = 0;
						//Debug.Log("Interaction Started");
						//Debug.Log("Selected Sphere: " + lastHitSphere);

					}
				}
			}

			if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
			{
				isInteracting = false;
				mixingController.MoveSpheresApart();
				//Debug.Log("Interaction Stopped");
			}

			if (isInteracting)
			{
				timer += Time.deltaTime;
				//Debug.Log("Is Interacting");

				if (swipeCount == 2)
				{
					mixingController.MoveSpheresTogether();
				}

				if (timer > timeForInteraction)
				{
					isInteracting = false;
					mixingController.MoveSpheresApart();
					//Debug.Log("Interaction Stopped");
				}
				else //if touch is moving 
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit))
					{
						if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
						{

							ColorNames hitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
							if (hitSphere != lastHitSphere)
							{
								timer = 0.0f;
								swipeCount++;
								lastHitSphere = hitSphere;
								//Debug.Log("Selected Sphere: " + lastHitSphere);
							}
						}
					}

				} 

			}
		}

#if UNITY_EDITOR

		if (activated)
			{
				if (Input.GetMouseButtonDown(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit))
					{
						if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
						{

							lastHitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
							isInteracting = true;
							timer = 0.0f;
							swipeCount = 0;
							//Debug.Log("Interaction Started");
							//Debug.Log("Selected Sphere: " + lastHitSphere);

						}
					}
				}

				if (Input.GetMouseButtonUp(0))
				{
					isInteracting = false;
					mixingController.MoveSpheresApart();
					//Debug.Log("Interaction Stopped");
				}

				if (isInteracting)
				{
					timer += Time.deltaTime;
					//Debug.Log("Is Interacting");

					if (swipeCount == 2)
					{
						mixingController.MoveSpheresTogether();
					}

					if (timer > timeForInteraction)
					{
						isInteracting = false;
						mixingController.MoveSpheresApart();
						//Debug.Log("Interaction Stopped");
					}
					else //if touch is moving (?)
					{
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						RaycastHit hit;

						if (Physics.Raycast(ray, out hit))
						{
							if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
							{

								ColorNames hitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
								if (hitSphere != lastHitSphere)
								{
									timer = 0.0f;
									swipeCount++;
									lastHitSphere = hitSphere;
									//Debug.Log("Selected Sphere: " + lastHitSphere);
								}
							}
						}

					} //else if touchIsNotMoving => intercation = false

				}
			} else
        {
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider != null && hit.collider.gameObject.tag == "ResultSphere")
					{

						gameManager.ToggleResultName();

					}
				}
			}
		}
#endif
		}

		public void Activate()
        {
			activated = true;
        }

        public void Deactivate()
        {
			activated = false;
        }



}

