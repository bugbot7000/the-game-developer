using UnityEngine;

using DG.Tweening;

namespace Michsky.DreamOS
{
    [RequireComponent(typeof(ButtonManager))]
    public class StorytellerItem : MonoBehaviour
    {
        [HideInInspector] public int itemIndex;
        [HideInInspector] public int layoutIndex;
        [HideInInspector] public ChatLayoutPreset layout;
        [HideInInspector] public MessagingManager msgManager;
        [HideInInspector] public DynamicMessageHandler handler;
        [HideInInspector] public string replyLocKey;

        Sequence mySequence;

        void Start()
        {
            mySequence = DOTween.Sequence().Append(transform.DOPunchScale(Vector3.one * 0.13f, 0.5f)).AppendInterval(4f).SetLoops(-1);
            
            ButtonManager strButton = gameObject.GetComponent<ButtonManager>();
            strButton.onClick.AddListener(delegate
            {
                string tempMsg = null;

                if (!string.IsNullOrEmpty(replyLocKey)) { tempMsg = replyLocKey; }
                else { tempMsg = msgManager.chatList[layoutIndex].chatAsset.storyTeller[msgManager.storyTellerIndex].replies[itemIndex].replyContent; }

                msgManager.HideStorytellerPanel();
                msgManager.CreateMessage(layout, tempMsg);
                msgManager.stItemIndex = itemIndex;
                msgManager.isStoryTellerOpen = false;

                if (!string.IsNullOrEmpty(msgManager.chatList[layoutIndex].chatAsset.storyTeller[msgManager.storyTellerIndex].replies[itemIndex].replyFeedback))
                {
                    handler.StartCoroutine(handler.HandleStoryTellerLatency(msgManager.chatList[layoutIndex].chatAsset.storyTeller[msgManager.storyTellerIndex].replies[itemIndex].feedbackLatency, layoutIndex, itemIndex));
                }

                for (int i = 0; i < msgManager.storytellerReplyEvents.Count; ++i)
                {
                    if (msgManager.storytellerReplyEvents[i].replyID == gameObject.name)
                    {
                        msgManager.storytellerReplyEvents[i].onReplySelect.Invoke();
                        break;
                    }
                }
            });
        }

        void OnDestroy()
        {
            mySequence.Kill();
        }
    }
}