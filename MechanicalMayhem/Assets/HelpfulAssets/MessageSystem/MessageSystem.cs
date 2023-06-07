using UnityEngine;
using SWAssets;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(ContentSizeFitter))]
[RequireComponent(typeof(VerticalLayoutGroup))]
public class MessageSystem : Singleton<MessageSystem> {

    [SerializeField] private int _maxMessageCount = 7;
	[SerializeField] private bool _test = false;

	private void Awake()
	{
		if (_test) InvokeRepeating(nameof(SendTestMessage), 0f, 1f);
	}

	private void SendTestMessage() => SendMessageToPlayer("Test", "This is a test message, to show that these are different, here is the time: " + Time.realtimeSinceStartup);

	public void SendMessageToPlayer(string sender, string message)
	{
        // Check for too many, if so delete
        if (transform.childCount >= _maxMessageCount)
		{
            Destroy(transform.GetChild(0).gameObject);
		}

		// Display the message
		TMP_Text clonedMessage = Instantiate(GameAssets.I.MessagePrefab, transform).GetComponent<TMP_Text>();
        clonedMessage.text = sender + ": " + message;

		if (clonedMessage.isTextOverflowing)
		{
			clonedMessage.autoSizeTextContainer = true;
		}
	}

}
