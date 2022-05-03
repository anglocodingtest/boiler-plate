
INSERT INTO [dbo].[Model]
           ([Name])
     VALUES
           ('Model 1')

INSERT INTO [dbo].[Model]
           ([Name]
           )
     VALUES
           ('Model 2')


INSERT INTO [dbo].[Commodity]
           ([Name])
     VALUES
           ('Commodity 1')

INSERT INTO [dbo].[Commodity]
           ([Name])
     VALUES
           ('Commodity 2')

		   INSERT INTO [dbo].[ModelCommodities]
           (ModelId, CommodityId,[VarAllocation])
     VALUES
           (1, 1, 8000000)

		   		   INSERT INTO [dbo].[ModelCommodities]
           (ModelId, CommodityId,[VarAllocation])
     VALUES
           (1, 2, 9000000)
		   		   		   INSERT INTO [dbo].[ModelCommodities]
           (ModelId, CommodityId,[VarAllocation])
     VALUES
           (2, 2, 10000000)


INSERT INTO [dbo].[DailyMetrics]
           ([Date]
           ,[Contract]
           ,[Price]
           ,[Position]
           ,[NewTradeAction]
           ,[PnlDaily]
           ,[ModelCommodityId])
    SELECT Date, Contract, Price, Position, New_Trade_Action, Pnl_Daily, 1
	FROM [dbo].[Model1_Commodity1]
GO

INSERT INTO [dbo].[DailyMetrics]
           ([Date]
           ,[Contract]
           ,[Price]
           ,[Position]
           ,[NewTradeAction]
           ,[PnlDaily]
           ,[ModelCommodityId])

    SELECT Date, Contract, Price, Position, New_Trade_Action, Pnl_Daily, 2
	FROM [dbo].[Model1_Commodity2]
GO

INSERT INTO [dbo].[DailyMetrics]
           ([Date]
           ,[Contract]
           ,[Price]
           ,[Position]
           ,[NewTradeAction]
           ,[PnlDaily]
		   ,[ModelCommodityId])

    SELECT Date, Contract, Price, Position, New_Trade_Action, Pnl_Daily, 3
	FROM [dbo].[Model2_Commodity2]
GO