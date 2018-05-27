using Assets.Scripts.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Sender
{

    public class CoordinateSender: MonoBehaviour
    {
        public float waitSec = 1;

        public void sendCoordiantes(ICollection<Coordinate> coordinates)
        {
            EventManager.onSpeedChanged += onSpeedChanged;
            StartCoroutine(DoSomething(coordinates));

        }

        private IEnumerator DoSomething(ICollection<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                EventManager.publishOnCoordinateSentEvent(coordinate);
                yield return new WaitForSeconds(waitSec);

            }
        }

        private void onSpeedChanged(float wait)
        {
            waitSec = wait;
        }
    }
}
