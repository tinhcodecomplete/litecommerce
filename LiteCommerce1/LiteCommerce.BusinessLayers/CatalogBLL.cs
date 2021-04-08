using LiteCommerce.DataLayers;
using System.Collections.Generic;
using LiteCommerce.DomainModel;
using System;
using System.Dynamic;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CatalogBLL 
    {
        private static ISupplierDAL SupplierDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }

        private static IProductDAL ProductDB { get; set; }

        private static IProductAttributeDAL ProductAttributeDB { get; set; }



        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SqlServer.SupplierDAL(connectionString);
            CustomerDB = new DataLayers.SqlServer.CustomerDAL(connectionString);
            ShipperDB = new DataLayers.SqlServer.ShipperDAL(connectionString);
            CategoryDB = new DataLayers.SqlServer.CategoryDAL(connectionString);
            ProductDB = new DataLayers.SqlServer.ProductDAL(connectionString);
            ProductAttributeDB = new DataLayers.SqlServer.ProductAttributeDAL(connectionString);
        }
        //------------------------------------supplier start---------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> Supplier_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;

            return SupplierDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// dem so supplier
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Supplier_Count(string searchValue)
        {
            return SupplierDB.Count(searchValue);
        }
        /// <summary>
        /// them 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Supplier_Add(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// xoa 1 hoac nhieu supplier
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public static bool Supplier_Delete(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        /// <summary>
        /// lay ve supplier theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier Supplier_Get(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        /// <summary>
        /// cap nhat supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Supplier_Update(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        public static List<Supplier> List_CompanyName_And_SupplierID()
        {
            return SupplierDB.List_CompanyName_And_SupplierID();
        }
        //-----------------------------------supplier end--------------------------------------------
        //-----------------------------------customer start------------------------------------------

        /// <summary>
        /// danh sach cac customer
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> Customer_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;

            return CustomerDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        /// dem so customer
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Customer_Count(string searchValue)
        {
            return CustomerDB.Count(searchValue);
        }
        /// <summary>
        /// them 1 customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Customer_Add(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// xoa 1 hoac nhieu customer
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        public static bool Customer_Delete(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }
        /// <summary>
        /// lay ve 1 customer theo id
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer Customer_Get(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// cap nhat customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Customer_Update(Customer data)
        {
            return CustomerDB.Update(data);
        }
        //-----------------------------------customer end---------------------------------------------
        //-----------------------------------category start-------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> Category_List(int page, int pageSize, string searchValue)
        {


            return CategoryDB.List(page, pageSize, searchValue);
        }
        public static int Category_Count(string searchValue)
        {
            return CategoryDB.Count(searchValue);
        }
        public static int Category_Add(Category data)
        {
            return CategoryDB.Add(data);
        }
        public static bool Category_Delete(int[] CategoryIDs)
        {
            return CategoryDB.Delete(CategoryIDs);
        }
        public static Category Category_Get(int CategoryID)
        {
            return CategoryDB.Get(CategoryID);
        }
        public static bool Category_Update(Category data)
        {
            return CategoryDB.Update(data);
        }
        public static List<Category> List_CategoryName_And_CategoryID()
        {
            return CategoryDB.List_CategoryName_And_CategoryID();
        }
        //---------------------------------category end-------------------------------------------------
       //---------------------------------shipper start------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> Shipper_List(int page, int pageSize, string searchValue)
        {
            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static int Shipper_Count(string searchValue)
        {
            return ShipperDB.Count(searchValue);
        }
        public static int Shipper_Add(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        public static bool Shipper_Delete(int[] ShipperIDs)
        {
            return ShipperDB.Delete(ShipperIDs);
        }
        public static Shipper Shipper_Get(int ShipperID)
        {
            return ShipperDB.Get(ShipperID);
        }
        public static bool Shipper_Update(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        public static List<Shipper> Shipper_List_Shipper_And_ShipperID()
        {
            return ShipperDB.List_Shipper_And_ShipperID();
        }

        //------------------------------------shipper end------------------------------------------------
        //------------------------------------product start-----------------------------------------------
        public static List<Product> Product_List(int page, int pageSize, string searchValue, int CategoryID, int SupplierID)
        {


            return ProductDB.List(page, pageSize, searchValue, CategoryID, SupplierID);
        }

        public static int Product_Count(string searchValue, int CategoryID, int SupplierID)
        {
            return ProductDB.Count(searchValue, CategoryID, SupplierID);
        }
        public static int Product_Add(Product data)
        {
            return ProductDB.Add(data);
        }
        public static bool Product_Delete(int[] ProductIDs)
        {
            return ProductDB.Delete(ProductIDs);
        }
        public static Product Product_Get(int productID)
        {
            return ProductDB.Get(productID);
        }
        public static bool Product_Update(Product data)
        {
            return ProductDB.Update(data);
        }

        //---------------------------------------- product end---------------------------------------------
        //---------------------------------------- productAttribute start----------------------------------
        public static List<ProductAttribute> ProductAttribute_List(int productID)
        {
            return ProductAttributeDB.List(productID);
        }

        
        public static int ProductAttribute_Add(ProductAttribute data)
        {
            return ProductAttributeDB.Add(data);
        }
        public static bool ProductAttribute_Delete(int[] AttributeIDs)
        {
            return ProductAttributeDB.Delete(AttributeIDs);
        }
        public static ProductAttribute ProductAttribute_Get(int AttributeID)
        {
            return ProductAttributeDB.Get(AttributeID);
        }
        public static bool ProductAttribute_Update(ProductAttribute data)
        {
            return ProductAttributeDB.Update(data);
        }
        public static bool ProductAttribute_DeleteByProductID(int productID)
        {
            return ProductAttributeDB.DeleteByProductID(productID);
        }
        //------------------------------------------productAttribute end----------------------------------
    }

}