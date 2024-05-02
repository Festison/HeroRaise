using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private IStoreController storeController;

    private string gem = "gem";
    private string noads = "noads";

    private void Start()
    {
        InitIAP();
    }

    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(gem, ProductType.Consumable);
        builder.AddProduct(noads, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("�ʱ�ȭ ����" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("�ʱ�ȭ ����" + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("���� ����");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("���� ���� :" + product.definition.id);

        if (product.definition.id == gem)
        {
            DataManager.Instance.PlayerItem.gem += 100;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void Purchase(string productID)
    {
        // ��ư �̺�Ʈ ����
        storeController.InitiatePurchase(productID);
    }

    private void CheckNonConsumable(string id)
    {
        // ���� ������ Ȯ��
        var product = storeController.products.WithID(id);

        if (product != null)
        {
            bool isCheck = product.hasReceipt;
        }
    }

}
