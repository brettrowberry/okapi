// Either requires Ionide 5 https://github.com/ionide/ionide-vscode-fsharp/issues/1305#issuecomment-726854574
// or in settings.json
// {
//     "FSharp.fsiExtraParameters": [
//         "--langversion:preview"
//     ]
// }

#r "nuget: Newtonsoft.Json,12.0.3"
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let serialize obj = 
    let ccns = CamelCaseNamingStrategy(OverrideSpecifiedNames = false)
    let dcr = DefaultContractResolver(NamingStrategy = ccns)
    let jss = JsonSerializerSettings(ContractResolver = dcr, Formatting = Formatting.Indented)
    JsonConvert.SerializeObject(obj, jss)

let desiredJson = System.IO.File.ReadAllText(__SOURCE_DIRECTORY__ + "/swagger.json")

type Info = {
    Title: string
    Description: string
    Version: string
}

// type Example = {
//     Body: string
// }

type OpenApiDoc = {
    Openapi: string
    Info: Info
}

let openApiDoc = {
    Openapi = "3.0.0"
    Info = {
        Title = "Okapi Title"
        Description = "Okapi Description"
        Version = "1.0.0"
    }
}

// let json = serialize openApiDoc

let writeOpenApiJson (openApiDoc : OpenApiDoc) =
    System.IO.File.WriteAllText("./WebRoot/okapi.json", serialize openApiDoc, System.Text.Encoding.UTF8)

writeOpenApiJson openApiDoc
