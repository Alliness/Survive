using System.Collections.Generic;
using Game.Scripts.DTO.Actor;
using UnityEngine;

namespace Game.Scripts.Actor
{
    public class SurvivorsManager : MonoBehaviour
    {
        public static SurvivorsManager instance;

        public GameObject spawnPoint;
        public GameObject survivorPrefab;

        private List<GameObject> _survivors;
        private Survivor activeSurvivor;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            _survivors = new List<GameObject>();
        }

        public List<GameObject> Survivors()
        {
            return _survivors;
        }

        public void SpawnSurvivor(SurvivorDTO dto)
        {
            GameObject survivor = Instantiate(survivorPrefab);
            survivor.name = dto.name + "_" + dto.lastName;
            survivor.transform.SetParent(transform, false);
            survivor.transform.position = spawnPoint.transform.position;


            // add script
            Survivor controller = survivor.GetComponent<Survivor>();

            // add model
            GameObject model = Instantiate(Resources.Load<GameObject>(dto.view.model), survivor.transform);
            model.transform.SetParent(survivor.transform);

            controller.Set(dto, model);
        }


        public void SetActiveSurvivor(Survivor survivor)
        {
            Unselect();
            activeSurvivor = survivor;
            //todo event controller delegate;
        }

        public void Unselect()
        {
            if (activeSurvivor != null)
            {
                activeSurvivor.UnSelect();
            }
        }
    }
}