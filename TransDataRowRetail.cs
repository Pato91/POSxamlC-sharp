public class TransDataRowRetail
{
    private string idRetail;
    private string barcodeRetail;
    private string pdtNameRetail;
    private string pdtDescription;
    private string pdtTypeRetail;
    private string packetQuantityRetail;
    private string RemainingQuantityRetail;
    private string pdtQuantityRetail;
    private string unitPriceRetail;
    private string totalAmountRetail;
    public TransDataRowRetail(string idRetail, string barcodeRetail, string pdtNameRetail, string pdtDescriptionRetail, string pdtTypeRetail, string packetQuantityRetail, string RemainingQuantityRetail, string pdtQuantityRetail, string unitPriceRetail, string totalAmountRetail)
    {
        this.idRetail = idRetail;
        this.barcodeRetail = barcodeRetail;
        this.pdtNameRetail = pdtNameRetail;
        this.pdtDescription = pdtDescriptionRetail;
        this.pdtTypeRetail = pdtTypeRetail;
        this.packetQuantityRetail = packetQuantityRetail;
        this.RemainingQuantityRetail = RemainingQuantityRetail;
        this.pdtQuantityRetail = pdtQuantityRetail;
        this.unitPriceRetail = unitPriceRetail;
        this.totalAmountRetail = totalAmountRetail;
    }

    public string Id
    {
        get { return this.idRetail; }
    }

    public string Barcode
    {
        get { return this.barcodeRetail; }
    }

    public string ProductName
    {
        get { return this.pdtNameRetail; }
    }

    public string ProductDescription
    {
        get { return this.pdtDescription; }
    }
    public string Type
    {
        get { return this.pdtTypeRetail; }
    }

    public string PacketQuantity
    {
        get { return this.packetQuantityRetail; }
    }

    public string RemainingQuantity
    {
        get { return this.RemainingQuantityRetail; }
    }
    public string Quantity
    {
        get { return this.pdtQuantityRetail; }
    }


    public string UnitPrice
    {
        get { return this.unitPriceRetail; }
    }
    public string TotalAmount
    {
        get { return this.totalAmountRetail; }
    }
}
