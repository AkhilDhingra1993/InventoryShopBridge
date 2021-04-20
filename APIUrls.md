# InventoryShopBridge
Inventory Shop Bridge

RUN ON POSTMAN

GET
https://localhost:44371/api/Products/
https://localhost:44371/api/Products/1
https://localhost:44371/api/Category/
https://localhost:44371/api/Category/1

Post
https://localhost:44371/api/Products/
{"ProductName": "Study Chair","ProductDescription": "Study Chair","Quantity_Available": 1222,"Price": 12367.12,"Product_Image": null,"Category_Id": 2}

https://localhost:44371/api/Category/
{"Category_Name": "Mobile" }

DELETE
https://localhost:44371/api/Products/1
https://localhost:44371/api/Category/4

Put/modify
https://localhost:44371/api/Products/2
https://localhost:44371/api/Category/2
