using static System.Console;

using GoldBadgeChallange.Data.Entities;
using GoldBadgeChallange.Repository.Customer_Repository;
using GoldBadgeChallange.Repository.Delivery_Repository;
using GoldBadgeChallange.Data.Entities.Enums;

public class ProgramUI
{
    private readonly CustomerRepository _customerRepo = new CustomerRepository();

    private readonly DeliveryRepository _deliveryRepo = new DeliveryRepository();

    private bool IsRunning = true;

    public void RunApplication()
    {
        Run();
    }

    public void Run()
    {
        while (IsRunning)
        {
            Clear();
            WriteLine("|=======================================|\n" +
                     "|                                       |\n" +
                     "|  Welcome to Warner Transit Federal's  |\n" +
                     "|  Delivery Managment Application       |\n" +
                     "|                                       |\n" +
                     "|=======================================|\n" +
                     "|                                       |\n" +
                     "|  What Would You Like To Do?           |\n" +
                     "|  1. Manage Deliveries                 |\n" +
                     "|  2. Manage Customers                  |\n" +
                     "|  0. Close Application                 |\n" +
                     "|                                       |\n" +
                     "|=======================================|");
            try
            {
                var userInput = int.Parse(Console.ReadLine()!);
                switch (userInput)
                {
                    case 1:
                        ManageDeliveries();
                        break;

                    case 2:
                        ManageCustomers();
                        break;

                    case 0:
                        IsRunning = ExitApplication();
                        break;

                    default:
                        WriteLine("Invalid Selection Please Try Again");
                        PressAnyKeyToContinue();
                        break;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Bad selection.. Press any key to continue");
                Console.ReadKey();
            }
        }
    }

    private void ManageDeliveries()
    {
        Clear();
        WriteLine("|=======================================|\n" +
                    "|                                       |\n" +
                    "|  Delivery Management Database         |\n" +
                    "|  What would you like to do with the   |\n" +
                    "|  deliveries?                          |\n" +
                    "|=======================================|\n" +
                    "|                                       |\n" +
                    "|  1. View All Deliveries               |\n" +
                    "|  2. View Delivery By Order Status     |\n" +
                    "|  3. Update Existing Delivery          |\n" +
                    "|  4. Add a New Delivery                |\n" +
                    "|  5. Delete a Delivey                  |\n" +
                    "|  6. View Deliveries by Customer       |\n" +
                    "|  0. Return to Main Menu               |\n" +
                    "|=======================================|");
        try
        {
            var userInput = int.Parse(Console.ReadLine()!);
            switch (userInput)
            {
                case 1:
                    ViewAllDeliveries();
                    break;

                case 2:
                    ViewDeliveryByOrderStatus();
                    break;

                case 3:
                    UpdateAnExistingDelivery();
                    break;

                case 4:
                    AddANewDelivery();
                    break;

                case 5:
                    DeleteExistingDelivery();
                    break;

                case 6:
                    ViewDeliveriesByCustomer();
                    break;

                case 0:
                    Run();
                    break;

                default:
                    System.Console.WriteLine("Invalid Entry Please Try Again");
                    PressAnyKeyToContinue();
                    break;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Bad selection.. Press any key to continue");
            Console.ReadKey();
        }
    }

    private void ViewDeliveriesByCustomer()
    {
        Clear();
        ViewDeliveryCustomerIdData();

        System.Console.WriteLine("Please choose a Customer Id to see deliveries for that Customer");
        int userInputCustomerId = int.Parse(ReadLine()!);
        var deliveryListing = _deliveryRepo.GetDeliveriesByCustomerId(userInputCustomerId);
        if (deliveryListing.Count > 0)
        {
            foreach (var delivery in deliveryListing)
            {
                DisplayDeliveryInfoForCustomerId(delivery);
            }
        }
        else
        {
            System.Console.WriteLine("Sorry no available deliveries");
        }
        PressAnyKeyToContinue();

    }

    private void ViewDeliveryCustomerIdData()
    {
        var customersInDb = _customerRepo.GetAllCustomers();
        if (customersInDb.Count > 0)
        {
            foreach (var customer in customersInDb)
            {
                DisplayCustomerInfo(customer);
            }
        }
        else
        {
            WriteLine("Sorry there are no available Customers.");
        }
    }

    private void DisplayCustomerInfo(Customer customer)
    {
        WriteLine($"Customer Id: {customer.CustomerId} | Customer Name: {customer.Name}");
    }

    private void DisplayDeliveryInfoForCustomerId(Delivery delivery)
    {

        System.Console.WriteLine("|                                                                                                                             |\n" +
                                $"| Delivery ID: {delivery.DeliveryId} | Customer ID: {delivery.CustomerId} | Order Date: {delivery.OrderDate} | Delivery Date: {delivery.DeliveryDate}                   |\n" +
                                "|                                                                                                                             |\n" +
                                "|-----------------------------------------------------------------------------------------------------------------------------|\n" +
                                "|                                                                                                                             |\n" +
                                $"| Status of Delivery: {delivery.OrderStatus} | Item Number: {delivery.ItemNumber} | Quantity Ordered: {delivery.ItemQuantity}                                                 |\n" +
                                "|                                                                                                                             |\n" +
                                "|=============================================================================================================================|");

    }

    // private Delivery GetDeliveriesForCustomerId(int userInputCustomerId)
    // {
    //     Delivery delivery = _deliveryRepo.GetDeliveriesByCustomerId(userInputCustomerId);
    //     return delivery;
    // }

    private void ViewDeliveryByOrderStatus()
    {
        Clear();
        System.Console.WriteLine("Please choose an order status to view deliveries by: \n" +
                                "1. Scheduled \n" +
                                "2. EnRoute\n" +
                                "3. Complete\n" +
                                "4. Canceled");
        int userInput = int.Parse(ReadLine()!);
        var orderStatus = (OrderStatus)userInput;
        var statusListing = _deliveryRepo.GetDeliveriesByOrderSatus(orderStatus);
        if (statusListing.Count > 0)
        {
            foreach (var delivery in statusListing)
            {
                DisplayDeliveryInformationForDelete(delivery);
            }
        }
        else
        {
            System.Console.WriteLine("Sorry no available deliveries");
        }
        PressAnyKeyToContinue();
    }

    private void RetrieveDeliveryData()
    {
        Clear();
        var deliverysInDb = _deliveryRepo.GetAllDeliveries();
        if (deliverysInDb.Count > 0)
        {
            foreach (var delivery in deliverysInDb)
            {
                DisplayDeliveryInformationForDelete(delivery);
            }
        }
        else
        {
            WriteLine("Sorry No available deliveries in the Database.");
        }
    }

    private void DeleteExistingDelivery()
    {
        Clear();
        WriteLine("Please select a Delivery by Delivery Id.");
        RetrieveDeliveryData();

        WriteLine("Please select a Delivery by Delivery Id.");
        int userInputDeliveryId = int.Parse(ReadLine()!);
        Delivery deliveryData = RetriveDeliveryDataInDb(userInputDeliveryId);

        if (deliveryData == null)
            DisplayDataValidationError(userInputDeliveryId);
        else
        {
            if (_deliveryRepo.DeleteADelivery(deliveryData))
                WriteLine($"Successfully deleted Delivery with ID: {userInputDeliveryId} !");
            else
                WriteLine("Fail!");

        }
        PressAnyKeyToContinue();
        ManageDeliveries();
    }

    private Delivery RetriveDeliveryDataInDb(int userInputDeliveryId)
    {
        Delivery delivery = _deliveryRepo.GetDeliveryById(userInputDeliveryId);
        return delivery;
    }

    private void DisplayDeliveryInformationForDelete(Delivery delivery)
    {
        System.Console.WriteLine("|============================================================================================================================|\n" +
                                $"| Delivery ID: {delivery.DeliveryId} | Customer ID: {delivery.CustomerId} | Status of Delivery: {delivery.OrderStatus}       |\n" +
                                "|                                                                                                                             |\n");
        System.Console.WriteLine("=============================================================================================================================");
    }

    private void AddANewDelivery()
    {
        bool hasFinishedAddingDelivery = false;
        while (!hasFinishedAddingDelivery)
        {
            Clear();
            Delivery deliverInformationData = InputDeliveryInformation();
            if (_deliveryRepo.AddDelivery(deliverInformationData))
                WriteLine("Successfully added the delivery to the database!");
            else
                WriteLine("Fail!");

            WriteLine("Do you need to add another deliery to the database? Please input y or n:");
            string userInputYesorNo = ReadLine()!;
            if (userInputYesorNo.ToLower() == "Y".ToLower())
            {
                continue;
            }
            else
            {
                System.Console.WriteLine("All new deliveries have been added to the database!");
                hasFinishedAddingDelivery = true;
            }
        }
        PressAnyKeyToContinue();
        ManageDeliveries();
    }

    private Delivery InputDeliveryInformation()
    {
        Clear();
        Delivery deliveryInformationData = new Delivery();
        bool isValidDate = false;
        while (!isValidDate)
        {
            System.Console.WriteLine("Is this delivery for an existing customer or new customer? \n" +
                                    "1. Existing Customer \n"+
                                    "2. New Customer\n");
            int userCusomerStatus = int.Parse(ReadLine()!);
            switch(userCusomerStatus)
            {
                case 1:
                    System.Console.WriteLine("Please enter Customer Id of customer for the Delivery");
                    ViewAllCustomers();
                    System.Console.WriteLine("Please enter Customer Id of customer for the Delivery");
                    int userInputsCustomerId = int.Parse(ReadLine()!);
                    deliveryInformationData.CustomerId = userInputsCustomerId;
                    Clear();
                    break;
                
                case 2:
                    AddANewCustomer();
                    ViewAllCustomers();
                    System.Console.WriteLine("Please enter Customer Id of the new customer for the Delivery");
                    int userInputsCustomerId2 = int.Parse(ReadLine()!);
                    deliveryInformationData.CustomerId = userInputsCustomerId2;
                    Clear();
                    break;

                default:
                    System.Console.WriteLine("Invalid selection please try agian.");
                    break;
            }

            Console.Write("Enter a delivery date for the Delivery (yyyy-MM-dd HH:mm:ss): ");
            string userInputDeliveryDate = Console.ReadLine()!;
            if (DateTime.TryParse(userInputDeliveryDate, out DateTime deliveryDate))
            {
                deliveryInformationData.DeliveryDate = deliveryDate;
                isValidDate = true;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date and time in the format yyyy-MM-dd HH:mm:ss.");
            }
        }

        bool isValidDate2 = false;
        while (!isValidDate2)
        {
            Write("Enter an order date for the Delivery (yyyy-MM-dd HH:mm:ss):  ");
            string userInputOrderDate = Console.ReadLine()!;
            if (DateTime.TryParse(userInputOrderDate, out DateTime orderDate))
            {
                deliveryInformationData.OrderDate = orderDate;
                isValidDate2 = true;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date and time in the format of yyyy-MM-dd HH:mm:ss.");
            }
        }

        Write("Please enter the Item Number for product ordered: ");
        string userInputItemNumber = ReadLine()!;
        deliveryInformationData.ItemNumber = userInputItemNumber;

        Write("Please enter how much product was ordered: ");
        int userInputItemQuantity = int.Parse(ReadLine()!);
        deliveryInformationData.ItemQuantity = userInputItemQuantity;

        Write("Please select the status of the delivery: \n" +
              "1. Scheduled\n" +
              "2. EnRoute\n" +
              "3. Complete\n" +
              "4. Canceled\n");
        try
        {
            int orderStatus = int.Parse(ReadLine()!);
            switch (orderStatus)
            {
                case 1:
                    deliveryInformationData.OrderStatus = OrderStatus.Scheduled;
                    break;

                case 2:
                    deliveryInformationData.OrderStatus = OrderStatus.EnRoute;
                    break;

                case 3:
                    deliveryInformationData.OrderStatus = OrderStatus.Complete;
                    break;

                case 4:
                    deliveryInformationData.OrderStatus = OrderStatus.Canceled;
                    break;
                default:
                    System.Console.WriteLine("Invalid selection please try again");
                    break;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Bad selection.. Press any key to continue");
            Console.ReadKey();
        }


        return deliveryInformationData;
    }

    private void UpdateAnExistingDelivery()
    {
        Clear();
        WriteLine("Which delivery needs to be updated?.");
        ViewDeliveriesThatCanBeUpdated();

        WriteLine("Please select a Delivery by the Delivery Id.");
        int userInputDeliveryId = int.Parse(ReadLine()!);
        Delivery deliveryDataInDb = RetriveDeliveryDataInDb(userInputDeliveryId);

        if (deliveryDataInDb == null)
            DisplayDataValidationError(userInputDeliveryId);
        else
        {
            Clear();
            WriteLine("== Update Delivery Information ==");
            Delivery newDeliveryData = InputDeliveryInformation();

            if (_deliveryRepo.UpdateADelivery(userInputDeliveryId, newDeliveryData))
                WriteLine("Successfully Updated Delivery !");
            else
                WriteLine("Failed to Updated Delivery!");
        }

        PressAnyKeyToContinue();
        ManageDeliveries();
    }

    private void ViewDeliveriesThatCanBeUpdated()
    {
        Console.Clear();
        System.Console.WriteLine("Which Delivery do you want to update?");
        System.Console.WriteLine("=================================================== Current Deliveries =================================================");
        var deliverysInDb = _deliveryRepo.GetAllDeliveries();
        if (deliverysInDb.Count > 0)
        {
            foreach (Delivery delivery in deliverysInDb)
            {
                System.Console.WriteLine("|                                                                                                                             |\n" +
                                        $"| Delivery ID: {delivery.DeliveryId} | Customer ID: {delivery.CustomerId} | Order Date: {delivery.OrderDate} | Delivery Date: {delivery.DeliveryDate}                   |\n" +
                                        "|                                                                                                                             |\n" +
                                        "|-----------------------------------------------------------------------------------------------------------------------------|\n" +
                                        "|                                                                                                                             |\n" +
                                        $"| Status of Delivery: {delivery.OrderStatus} | Item Number: {delivery.ItemNumber} | Quantity Ordered: {delivery.ItemQuantity}                                                 |\n" +
                                        "|                                                                                                                             |\n" +
                                        "|=============================================================================================================================|");
            }
        }
    }

    private void ViewDeliveryById()
    {

    }

    private void ViewAllDeliveries()
    {
        Console.Clear();
        System.Console.WriteLine("=================================================== Current Deliveries =================================================");
        var deliverysInDb = _deliveryRepo.GetAllDeliveries();
        if (deliverysInDb.Count > 0)
        {
            foreach (Delivery delivery in deliverysInDb)
            {
                System.Console.WriteLine("|                                                                                                                             |\n" +
                                        $"| Delivery ID: {delivery.DeliveryId} | Customer ID: {delivery.CustomerId} | Order Date: {delivery.OrderDate} | Delivery Date: {delivery.DeliveryDate}                   |\n" +
                                        "|                                                                                                                             |\n" +
                                        "|-----------------------------------------------------------------------------------------------------------------------------|\n" +
                                        "|                                                                                                                             |\n" +
                                        $"| Status of Delivery: {delivery.OrderStatus} | Item Number: {delivery.ItemNumber} | Quantity Ordered: {delivery.ItemQuantity}                                                 |\n" +
                                        "|                                                                                                                             |\n" +
                                        "|=============================================================================================================================|");
            }
        }
        PressAnyKeyToContinue();
        Console.ReadKey();
        ManageDeliveries();
    }

    private void ManageCustomers()
    {
        Clear();
        WriteLine("|=======================================|\n" +
                    "|                                       |\n" +
                    "|  Customer Management Database         |\n" +
                    "|  What would you like to do with the   |\n" +
                    "|  deliveries?                          |\n" +
                    "|=======================================|\n" +
                    "|                                       |\n" +
                    "|  1. View All Customers                |\n" +
                    "|  2. View Specific Customer            |\n" +
                    "|  3. Add a New Customer                |\n" +
                    "|  4. Delete a Customer                 |\n" +
                    "|  5. View Deliveries by Customer       |\n" +
                    "|  0. Return to Main Menu               |\n" +
                    "|=======================================|");
        try
        {
            var userInput = int.Parse(Console.ReadLine()!);
            switch (userInput)
            {
                case 1:
                    ViewAllCustomers();
                    break;

                case 2:
                    ViewCustomerByCustomerId();
                    break;

                case 3:
                    AddANewCustomer();
                    break;

                case 4:
                    DeleteExistingCustomer();
                    break;

                case 5:
                    ViewDeliveriesByCustomer();
                    break;

                case 0:
                    Run();
                    break;

                default:
                    System.Console.WriteLine("Invalid Entry Please Try Again");
                    PressAnyKeyToContinue();
                    break;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Bad selection.. Press any key to continue");
            Console.ReadKey();
        }
    }

    private void ViewAllCustomers()
    {
        Clear();

        WriteLine("===== Current Customers =====");

        RetrieveCustomerInformationForAllCustomers();
        PressAnyKeyToContinue();
        ReadKey();
        // ManageCustomers();
    }

    private void AddANewCustomer()
    {
        bool hasFinishedAddingCustomer = false;
        while (!hasFinishedAddingCustomer)
        {
            Clear();
            Customer customerInformationData = InputCustomerInformation();
            if (_customerRepo.AddCustomer(customerInformationData))
                WriteLine("Successfully added the Customer to the database!");
            else
                WriteLine("Fail!");

            WriteLine("Do you need to add another Customer to the database? Please input y or n: ");
            string userInputYesorNo = ReadLine()!;
            if (userInputYesorNo.ToLower() == "Y".ToLower())
            {
                continue;
            }
            else
            {
                System.Console.WriteLine("All new customers have been added to the database!");
                hasFinishedAddingCustomer = true;
            }
        }
        PressAnyKeyToContinue();
        // ManageCustomers();
    }

    private Customer InputCustomerInformation()
    {
        Clear();
        Customer newCustomer = new Customer();
        Console.Write("Enter the Customer Name: ");
        string userInputCustomerName = Console.ReadLine()!;
        newCustomer.Name = userInputCustomerName;
        return newCustomer;
    }

    private void ViewCustomerByCustomerId()
    {
        Clear();

        RetrieveCustomerInformationForId();

        WriteLine("Please Select a Customer Id.");
        int userInputCustomerId = int.Parse(ReadLine()!);
        Customer customer = RetrieveCustomerNameBasedOnId(userInputCustomerId);

        if (customer == null)
            DisplayDataValidationError(userInputCustomerId);
        else
        {
            Clear();
            DisplayCustomerInfo(customer!);
        }

        PressAnyKeyToContinue();
        ManageCustomers();
    }

    private void DisplayCustomerIds(Customer customer)
    {
        WriteLine($"Customer Id: {customer.CustomerId}");
    }

    private void RetrieveCustomerInformationForAllCustomers()
    {
        var customersInDb = _customerRepo.GetAllCustomers();
        if (customersInDb.Count > 0)
        {
            foreach (var customer in customersInDb)
            {
                DisplayCustomerInfo(customer);
            }
        }
        else
        {
            WriteLine("Sorry there are no available Customers.");
        }
    }

    private void RetrieveCustomerInformationForId()
    {
        var customersInDb = _customerRepo.GetAllCustomers();
        if (customersInDb.Count > 0)
        {
            foreach (var customer in customersInDb)
            {
                DisplayCustomerIds(customer);
            }
        }
        else
        {
            WriteLine("Sorry there are no available Customers.");
        }
    }

    private Customer RetrieveCustomerNameBasedOnId(int userInputCustomerId)
    {
        Customer customer = _customerRepo.GetCustomerById(userInputCustomerId);
        return customer;
    }

    private void DeleteExistingCustomer()
    {
        Clear();
        WriteLine("Please select a Customer by Customer Id.");
        RetrieveCustomerInformationForAllCustomers();

        WriteLine("Please select a Customer by Customer Id.");
        int userInputCustomerId = int.Parse(ReadLine()!);
        Customer customerData = RetrieveCustomerNameBasedOnId(userInputCustomerId);

        if (customerData == null)
            DisplayDataValidationError(userInputCustomerId);
        else
        {
            if (_customerRepo.DeleteCustomer(customerData))
            WriteLine($"Successfully deleted Customer with ID: {userInputCustomerId} !");
            WriteLine("Do you want to remove the customer's delivery(s) from the database?");
            string userInputYesorNo = ReadLine()!;
            if (userInputYesorNo.ToLower() == "Y".ToLower())
            {
                bool doneDeletingDelivery = false;
                while (!doneDeletingDelivery)
                {
                    var deliveryListing = _deliveryRepo.GetDeliveriesByCustomerId(userInputCustomerId);
                    if (deliveryListing.Count > 0)
                    {
                        foreach (var delivery in deliveryListing)
                        {
                            DisplayDeliveryInfoForCustomerId(delivery);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Sorry no available deliveries");
                        PressAnyKeyToContinue();
                        ManageCustomers();
                    }
                    WriteLine("Please select a Delivery by Delivery Id.");
                    int userInputDeliveryId = int.Parse(ReadLine()!);
                    Delivery deliveryData = RetriveDeliveryDataInDb(userInputDeliveryId);

                    if (deliveryData == null)
                        DisplayDataValidationError(userInputDeliveryId);
                    else
                    {
                        if (_deliveryRepo.DeleteADelivery(deliveryData))
                        {    
                            WriteLine($"Successfully deleted Delivery with ID: {userInputDeliveryId} !");
                        }
                        else
                        {
                            WriteLine("Fail!");
                        }
                        WriteLine("Do you need to add another deliery to the database? Please input y or n:");
                        string userInputYesorNo2 = ReadLine()!;
                        if (userInputYesorNo2.ToLower() == "Y".ToLower())
                        {
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("All new deliveries have been added to the database!");
                            doneDeletingDelivery = true;
                        }

                    }
                }
            }
            else
                WriteLine("Fail!");

        }
        PressAnyKeyToContinue();
        ManageCustomers();
    }

    private bool ExitApplication()
    {
        return false;
    }

    private void DisplayDataValidationError(int userInputValue)
    {
        ForegroundColor = ConsoleColor.Blue;
        WriteLine($"Invalid Id Entry: {userInputValue}!");
        ResetColor();
        return;
    }

    private void PressAnyKeyToContinue()
    {
        WriteLine("Press any key to continue...");
        ReadKey();
    }

}