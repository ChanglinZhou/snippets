open System
open System.ServiceModel
open System.ServiceModel.Description
open System.Runtime.Serialization
open System.Diagnostics

[<DataContract(Namespace="http://www.someplace.com/WCFDemo")>]
type Name() =
    let mutable _firstName : string = String.Empty
    let mutable _lastName : string = String.Empty
    let mutable _middleInitial : Char = ' '

    [<DataMember>]
    member public l.FirstName
        with get() = _firstName
        and set(value) = _firstName <- value

    [<DataMember>]
    member public l.LastName
        with get() = _lastName
        and set(value) = _lastName <- value

    [<DataMember>]
    member public l.MiddleInitial
        with get() = _middleInitial
        and set(value) = _middleInitial <- value

[<ServiceContract(Namespace=
    "http://www.someplace.com/WCFDemo")>]
type ISimpleService =
    [<OperationContract>]
    abstract member MyRequestReplyMessage : fullName : Name -> string

    [<OperationContract(IsOneWay=true)>]
    abstract member MyOneWayMessage : someInt : int * someBool : bool -> unit

type SimpleService() =
    interface ISimpleService with
        member x.MyRequestReplyMessage(fullName) =
            String.Format("{0} {1} {2}",
                fullName.FirstName,
                fullName.MiddleInitial,
                fullName.LastName)

        member x.MyOneWayMessage(someInt, someBool) =
            Debug.WriteLine(String.Format
                ("someInt: {0}, someBool: {1}",
                 someInt,
                 someBool))
            ()

let main () =
    let baseAddress = Uri("http://localhost:12345/wcfsharp")
    use host = new ServiceHost(typeof<SimpleService>, baseAddress)
    
    // Enable metadata publishing.
    let smb = new ServiceMetadataBehavior()
    smb.HttpGetEnabled <- true;
    smb.MetadataExporter.PolicyVersion <- PolicyVersion.Policy15;
    host.Description.Behaviors.Add(smb);

    // Open the ServiceHost to start listening for messages. Since
    // no endpoints are explicitly configured, the runtime will create
    // one endpoint per base address for each service contract implemented
    // by the service.
    host.Open();

    Console.WriteLine("The service is ready at {0}", baseAddress)
    Console.WriteLine("Press <Enter> to stop the service.")
    let _ = Console.ReadLine()

    // Close the ServiceHost.
    host.Close()
    ()

main ()