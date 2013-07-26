open System
open System.ServiceModel
open System.Runtime.Serialization
open System.Net

[<DataContract(Namespace="http://www.scottseely.com/WCFDemo")>]
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
    "http://www.scottseely.com/WCFDemo")>]
type ISimpleService =
    [<OperationContract>]
    abstract member MyRequestReplyMessage : fullName : Name -> string

    [<OperationContract(IsOneWay=true)>]
    abstract member MyOneWayMessage : someInt : int * someBool : bool -> unit

let main () =
    let endpoint = new EndpointAddress(new Uri("http://localhost:12345/wcfsharp"))
    let binding = new BasicHttpBinding()
    let factory = new ChannelFactory<ISimpleService>(binding, endpoint)
    let client = factory.CreateChannel()
    let name = Name()
    name.FirstName <- "Changlin"
    name.LastName <- "Zhou"
    let result = client.MyRequestReplyMessage name
    ()

main ()