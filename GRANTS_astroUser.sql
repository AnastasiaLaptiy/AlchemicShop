﻿--ACCESS ON SELECT USERS
USE [AlchemicShopDb]
GO
GRANT SELECT ON [USERS] to [astroUser]

--ACCESS ON SELECT ON ORDERPRODUCTS
USE [AlchemicShopDb]
GO
GRANT SELECT, DELETE ON [ORDERPRODUCTS] to [astroUser]

--ACCESS EXECUTE SP_Count_User_Without_Order
USE [AlchemicShopDb]
GO
GRANT EXECUTE ON [SP_Count_User_Without_Order] to [astroUser]

--ACCESS DDL ON CATEGORIES
USE [AlchemicShopDb]
GO
GRANT ALTER ON [CATEGORIES] to [astroUser]

--ALTER TABLE [CATEGORIES]
--ADD test_col int;

--ALTER TABLE [CATEGORIES]
--DROP COLUMN test_col;


