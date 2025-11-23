using System.ComponentModel;

namespace KSProject.Common.Constants.Enums
{
    public enum PaymentGatewayTypes
    {
        [Description("ارز دیجیتال")]
        Crypto = 0,
        [Description("زرین پال")]
        ZarinPal = 1,
        [Description("بانک ملت")]
        Mellat = 2,
        [Description("بانک پاسارگاد")]
        Pasargad = 3
    }
}
