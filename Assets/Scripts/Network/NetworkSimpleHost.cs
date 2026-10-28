using Unity.Netcode;
using UnityEngine;

namespace NetworkSimple
{
	/// <summary>
	/// Add this component to the same GameObject as
	/// the NetworkManager component.
	/// </summary>
	public class NetworkSimpleHost : MonoBehaviour
	{
		private NetworkManager m_NetworkManager;

		private void Awake()
		{
			m_NetworkManager = GetComponent<NetworkManager>();
		}

		private void OnGUI()
		{
			GUILayout.BeginArea(new Rect(10, 10, 500, 500));
			if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
			{
				StartButtons();
			}
			else
			{
				StatusLabels();

				SubmitNewPosition();
			}

			GUILayout.EndArea();
		}

		private void StartButtons()
		{
			GUI.skin.button.fontSize = 100;
			GUI.skin.label.fontSize = 50;
			if (GUILayout.Button("Host")) m_NetworkManager.StartHost();
			if (GUILayout.Button("Client")) m_NetworkManager.StartClient();
			if (GUILayout.Button("Server")) m_NetworkManager.StartServer();
		}

		private void StatusLabels()
		{
			var mode = m_NetworkManager.IsHost ?
				"Host" : m_NetworkManager.IsServer ? "Server" : "Client";

			GUILayout.Label("Transport: " +
				m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
			GUILayout.Label("Mode: " + mode);
		}

		private void SubmitNewPosition()
		{
			if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
			{
				if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
				{
					foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
						m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Rtp();
				}
				else
				{
					var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
					var player = playerObject.GetComponent<HelloWorldPlayer>();
					player.Rtp();
				}
			}
		}
	}
}
