--ใส่รหัส ItemID ให้รายการ และ Update รายการเดิมทั้งหมด
Go
Update CostsDetail Set ItemID = newid() Where ItemID Is Null

Go
Update CostsDetail Set ItemID = newid() Where ItemID = ''

Go
Update ActivityCostsDetail 
Set ActivityCostsDetail.ItemID = (CostsDetail.ItemID) 
From ActivityCostsDetail inner join CostsDetail 
On ActivityCostsDetail.ActivityCode = CostsDetail.ActivityCode
And ActivityCostsDetail.EntryCostsCode = CostsDetail.ListName