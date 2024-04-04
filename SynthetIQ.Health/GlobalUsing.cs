//***********************************************************************
// 3RD PARTY (SEPERATED BY TOP-LEVEL NAMESPACE AND SORTED ALPHABETICALLY)
//***********************************************************************

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Diagnostics.HealthChecks;

// For future Chaos policy implementation - https://github.com/Polly-Contrib/Simmy#Inject-fault
// global using Polly.Contrib.Simmy;

//********************************
// INTERNAL (THIS ASSEMBLY)
//********************************

global using System.Diagnostics;

//********************************
// MF LIBRARIES AND PROJECTS
//********************************
global using MF.DomainName.Health.Value.Constants;

global using Quickwire;
global using Quickwire.Attributes;

global using System.Collections.Generic;
global using System.IO;
global using System.Threading;
global using System.Threading.Tasks;