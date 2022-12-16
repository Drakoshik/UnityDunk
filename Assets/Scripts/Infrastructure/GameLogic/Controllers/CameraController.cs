using System.Collections;
using UnityEngine;

namespace Infrastructure.GameLogic.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;

        public void SetFollow(GameObject mainBall)
        {
            _virtualCamera.transform.position = Vector3.zero;
            Camera.main.transform.position = Vector3.zero;
            StartCoroutine(FollowBall(mainBall));
        }

        private IEnumerator FollowBall(GameObject mainBall)
        {
            yield return new WaitForSeconds(.1f);
            _virtualCamera.Follow = mainBall.transform;
        }

        public void ResetFollow()
        {
            _virtualCamera.Follow = null;
        }

    }
}
