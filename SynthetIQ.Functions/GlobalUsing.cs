//********************************************************************************************
// BP:1 - All using statements should reside in a Global.cs file in the project root.

// BP:2 - All using statements should be sorted alphabetically (use the CodeMaid extension)
//********************************************************************************************

//************
// SYSTEM
//************
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Net.Http.Headers;
global using System.Net.Http;
global using System.Net;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.Text;
global using System.Threading.Tasks;
global using System.Threading;

//************
// 3RD PARTY
//************
global using Azure.Storage.Blobs;

global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Azure.Functions.Worker.Http;
global using Microsoft.Azure.Functions.Worker.Middleware;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Options;

global using Newtonsoft.Json;

global using OpenAI;
global using OpenAI.Managers;
global using OpenAI.ObjectModels;
global using OpenAI.ObjectModels.RequestModels;

global using Quickwire;
global using Quickwire.Attributes;

//********************************
// INTERNAL (THIS ASSEMBLY)
//********************************
global using SynthetIQ.Domain.Value.Constant;
global using SynthetIQ.Function.Domain.Repository.API;
global using SynthetIQ.Interface.Repository.API;
global using SynthetIQ.Interface.Value.Request;
global using SynthetIQ.Interface.Value.Response;
global using SynthetIQ.Utility.Exception;
global using SynthetIQ.Utility.Helpers;
global using SynthetIQ.Functions.Domain.Value.Shared;
global using SynthetIQ.Functions.Domain.Value.DTO;
global using SynthetIQ.Functions.Domain.Value.Helpers;
global using SynthetIQ.DbContext.Models;

//********************************
// ALIASES (FOR CLARITY)
//********************************
global using JsonSerializer = System.Text.Json.JsonSerializer;

// BP:3 -Use System.Text.Json instead of NewtonSoft. It is more efficient than NewtonSoft as it is
// faster, produces smaller payloads, and is simpler to use.