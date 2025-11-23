namespace KSProject.Common.Constants.Enums
{
    public enum TransactionTypes
    {
        Usage = 0,
        Charge = 1,
        Refund = 2,      // برگشت پول (مثل در صورت cancel subscription در Online Learning)
        Adjustment = 3,  // تصحیح دستی (مثل admin adjustment در Mentoring sessions)
        Transfer = 4
    }
}
