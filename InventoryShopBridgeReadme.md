# InventoryShopBridge
Inventory Shop Bridge

#includes 2 projects :
1. WEB API Project - ShopBridge
2. Class Liberary  - InventoryDataAccess 

#Used SQL server connection 2019
It contails below table :
**Category**
=> ID - primary key(Foreign key for Products table)
=> Category_Name

**Products**
=> Id                 
=> ProductName        
=> ProductDescription 
=> Quantity_Available 
=> Price              
=> Product_Image      
=> Category_Id  (primary key for category table) 


**Handlings**
1. Added the Exception handling throughout the Project
2. Added the Validations of Required in Entities : Category and Products

Below API's to be run on Postmanor any other tool. 

For GET/Fetching the products
https://localhost:44371/api/Products/
https://localhost:44371/api/Products/1

For GET/Fetching the Category
https://localhost:44371/api/Category/
https://localhost:44371/api/Category/1

For Posting the data to Products
https://localhost:44371/api/Products/
{"ProductName": "Study Chair","ProductDescription": "Study Chair","Quantity_Available": 1222,"Price": 12367.12,"Product_Image": null,"Category_Id": 2}

For Posting the data to Category
https://localhost:44371/api/Category/
{"Category_Name": "Mobile" }

DELETE the product
https://localhost:44371/api/Products/1

DELETE the category
https://localhost:44371/api/Category/4

Put/modify
https://localhost:44371/api/Products/2
https://localhost:44371/api/Category/2
