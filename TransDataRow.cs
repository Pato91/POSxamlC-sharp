public class TransDataRow
{
    private string id;
    private string barcode;
    private string pdtName;
    private string pdtDescription;
    private string pdtType;
    private string inventoryQuantity;
    private string pdtQuantity;
    private string unitPrice;
    private string totalAmount;
    public TransDataRow(string id, string barcode, string pdtName, string pdtDescription, string pdtType, string inventoryQuantity, string pdtQuantity, string unitPrice, string totalAmount)
    {
        this.id = id;
        this.barcode = barcode;
        this.pdtName = pdtName;
        this.pdtDescription = pdtDescription;
        this.pdtType = pdtType;
        this.inventoryQuantity = inventoryQuantity;
        this.pdtQuantity = pdtQuantity;
        this.unitPrice = unitPrice;
        this.totalAmount = totalAmount;
    }

    public string Id
    {
        get { return this.id; }
    }

    public string Barcode
    {
        get { return this.barcode; }
    }

    public string ProductName
    {
        get { return this.pdtName; }
    }

    public string ProductDescription
    {
        get { return this.pdtDescription; }
    }
    public string Type
    {
        get { return this.pdtType; }
    }

    public string InventoryQuantity
    {
        get { return this.inventoryQuantity; }
    }
    public string Quantity
    {
        get { return this.pdtQuantity; }
    }

    public string UnitPrice
    {
        get { return this.unitPrice; }
    }
    public string TotalAmount
    {
        get { return this.totalAmount; }
    }
}


