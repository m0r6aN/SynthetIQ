﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SynthetIQ.DbContext.Models;

public partial class Llm
{
    public int LlmconfigurationId { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string Endpoints { get; set; }

    public string Apikey { get; set; }

    public int? AssistantId { get; set; }

    public virtual Assistant Assistant { get; set; }
}