using Photon.Pun;
using TMPro;

public class PlayerNameTag : MonoBehaviourPun
{
    private TextMeshPro nameText;
    void Start()
    {
        nameText = gameObject.GetComponent<TextMeshPro>();
        if (photonView.IsMine)
        {
            nameText.gameObject.SetActive(false);
            return;
        }
        SetName();
    }

    private void SetName() => nameText.text = photonView.Owner.NickName;
}