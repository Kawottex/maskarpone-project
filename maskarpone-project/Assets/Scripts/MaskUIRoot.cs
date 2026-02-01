using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskUIRoot : MonoBehaviour
{
    [SerializeField]
    private GameObject m_twoMasksRoot;

    [SerializeField]
    private GameObject m_threeMasksRoot;

    [SerializeField]
    private GameObject m_fourMasksRoot;

    [SerializeField]
    private FlowManager m_flowManager;

    private GameObject m_currentPanel = null;

    private void Awake()
    {
        HidePanelButtons(m_twoMasksRoot);
        HidePanelButtons(m_threeMasksRoot);
        HidePanelButtons(m_fourMasksRoot);
    }

    private void HidePanelButtons(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            RectTransform rectTransform = panel.transform.GetChild(i).GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.zero;
        }
    }

    public void DisplayMasks(List<MaskSO> maskList)
    {
        DisableAllPanels();

        switch (maskList.Count)
        {
            case 2:
                m_currentPanel = m_twoMasksRoot;
                break;
            case 3:
                m_currentPanel = m_threeMasksRoot;
                break;
            case 4:
                m_currentPanel = m_fourMasksRoot;
                break;
            default:
                Debug.LogError("Incorrect number of masks to display: " + maskList.Count);
                break;
        }

        m_currentPanel.SetActive(true);
        StartCoroutine(ShowButtons(maskList));
    }

    private void DisableAllPanels()
    {
        m_twoMasksRoot.SetActive(false);
        m_threeMasksRoot.SetActive(false);
        m_fourMasksRoot.SetActive(false);
    }

    private IEnumerator ShowButtons(List<MaskSO> maskList)
    {
        for (int i = 0; i < maskList.Count; i++)
        {
            Transform panelChild = m_currentPanel.transform.GetChild(i);
            panelChild.GetChild(0).GetComponent<Image>().sprite = maskList[i].MaskIcon;
            BindButton(panelChild.GetComponent<Button>(), maskList[i]);

            Animator buttonAnimator = panelChild.GetComponent<Animator>();
            buttonAnimator.SetTrigger("FadeIn");

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void BindButton(Button buttonToBind, MaskSO maskBind)
    {
        buttonToBind.onClick.AddListener(() => OnMaskSelected(maskBind));
    }

    private void OnMaskSelected(MaskSO selectedMask)
    {
        StartCoroutine(m_flowManager.TriggerMaskSelected(selectedMask));
        StartCoroutine(HideButtons());
    }

    private IEnumerator HideButtons()
    {
        for (int i = 0; i < m_currentPanel.transform.childCount; i++)
        {
            Animator buttonAnimator = m_currentPanel.transform.GetChild(i).GetComponent<Animator>();
            buttonAnimator.SetTrigger("FadeOut"); 
            yield return new WaitForSeconds(0.5f);
        }
    }
}
