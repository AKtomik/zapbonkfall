using Unity.Netcode;
using UnityEngine;

namespace NetworkSimple
{
	public class HelloWorldPlayer : NetworkBehaviour
	{
		public NetworkVariable<Vector3> NetPosition = new NetworkVariable<Vector3>();

		public override void OnNetworkSpawn()
		{
			if (IsOwner)
			{
				Rtp();
			}
		}

		public void Rtp()
		{
			SubmitPositionRequestRpc();
		}

		[Rpc(SendTo.Server)]
		private void SubmitPositionRequestRpc(RpcParams rpcParams = default)
		{
			var randomPosition = GetRandomPositionOnPlane();
			transform.position = randomPosition;
			NetPosition.Value = randomPosition;
		}

		static Vector3 GetRandomPositionOnPlane()
		{
			return new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
		}

		private void Update()
		{
			transform.position = NetPosition.Value;
		}
	}
}
