# Dir
![image](https://user-images.githubusercontent.com/58219087/146987724-fc9fdc15-36b9-4bd4-9886-17c541d497e6.png)
# WinForms Application
## Final version (Uncompleted)
- This application requires local database to be loaded, as well as triggers, procedures,...
- This application is not completed yet, but has no plans to be develop in the future. Well, we stepped on ours deadline.
- Open source for education purpose.
### Login
![image](https://user-images.githubusercontent.com/58219087/146988226-063f78bf-5296-4fd5-98a4-983031efdf9a.png)
### Sign Up
#### As A Customer
![image](https://user-images.githubusercontent.com/58219087/146988284-c4b95f7a-39f7-49e5-897e-c3a5c72e4933.png)
#### Sign Up To Be A Shipper
![image](https://user-images.githubusercontent.com/58219087/146988429-18cbbd6f-4a04-43dc-a862-454b366e5da0.png)
#### Sign Up To Be A Partner
![image](https://user-images.githubusercontent.com/58219087/146988589-239fc824-fd01-491d-96e2-25bfa443441e.png)


### Profile (As A Customer)
![image](https://user-images.githubusercontent.com/58219087/146988697-510d8ea3-36ea-4f8b-8c5d-b1a77f40d69f.png)
### Products Manage
![image](https://user-images.githubusercontent.com/58219087/146988853-610d1d0f-96dc-487d-a083-f60ff7fb235e.png)
### Orders Manage
![image](https://user-images.githubusercontent.com/58219087/146989011-02aba267-80bd-4cd6-a24f-99bf7c90380d.png)
### Customer Manage
![image](https://user-images.githubusercontent.com/58219087/146989079-a7752c33-1804-462c-8060-c2007d7fbe86.png)
### Staff Manage
![image](https://user-images.githubusercontent.com/58219087/147130277-4d3fee7e-d2e0-495c-a9a0-32d7d4196a9f.png)
### Shipper Manage
![image](https://user-images.githubusercontent.com/58219087/146989131-3aee443f-2870-48e9-ab40-bc1c76babcb2.png)
### Partner Manage
![image](https://user-images.githubusercontent.com/58219087/146989181-accc52c0-7686-4d6b-a51b-9dd76232cc2a.png)

# Concurrency Demo
## Dirty Read
### Problem
- Staff updates price for all products of a department, a customer request to read all product of this department at the same time, too.
- Well, after the customer read data, a issue occured that staff's transaction has to be rolled back.
![image](https://user-images.githubusercontent.com/58219087/146990383-d322a67d-b787-4c09-b227-70a62ae48099.png)
### Handled
- Customer has to wait until the staff's transaction is commited or rolled back. (??? :D How silly)
![image](https://user-images.githubusercontent.com/58219087/146991340-f6a8a5cf-d05e-4b23-b364-52caa7f29906.png)
## Unreapeatable Read
### Problem
- Shipper want to search specified products quantity of an order
- But the customer makes change of quantity at the same time.
![image](https://user-images.githubusercontent.com/58219087/146991807-a3f9e0b3-9f1a-4f34-a950-cbd0bc22a0d9.png)
### Handled
- Customer has to waits until shipper's transaction is finished.
- ![image](https://user-images.githubusercontent.com/58219087/146991969-73007e2d-d3bc-48bf-b663-05e40242621f.png)
## Phantom Read
### Problem
- Customer search all products have price bigger than given value.
- Staff added a new product has price in range which to be searched by customer.
- Staff's transaction is rolled back.
![image](https://user-images.githubusercontent.com/58219087/146992517-3074ff30-8eee-485d-85b5-3d4ec315ba2b.png)
### Handled
- Staff has to wait for customer.
![image](https://user-images.githubusercontent.com/58219087/146992772-f83a572c-d2b6-47ff-ab63-2847de8ca848.png)
