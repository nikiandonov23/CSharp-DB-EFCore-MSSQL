using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Medicines.DataProcessor.ImportDtos;

public class ImportPatientMedicinesDto
{
    [JsonProperty(nameof(FullName))]  
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;  //VALIDIRANO



    [JsonProperty(nameof(AgeGroup))]
    [Required]
    [Range(0,2)]
    public int AgeGroup { get; set; }  //ENUM VALIDIRANA




    [JsonProperty(nameof(Gender))]
    [Required]
    [Range(0,1)]
    public int Gender { get; set; }   //enum VALIDIRANA




    [JsonProperty(nameof(Medicines))] // NE
    [Required]
    public int[] Medicines { get; set; } = null!;
}