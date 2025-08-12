using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Data.Models;
using System.ComponentModel.DataAnnotations;

public class TourPackageGuide
{
    //	TourPackageId – integer, Primary Key, foreign key(required)
    //  TourPackage – TourPackage
    //	GuideId – integer, Primary Key, foreign key(required)
    //  Guide – Guide

    [ForeignKey(nameof(TourPackage))]
    public int TourPackageId { get; set; }
    public virtual TourPackage TourPackage { get; set; } = null!;



    [ForeignKey(nameof(Guide))]
    public int GuideId { get; set; }
    public virtual Guide Guide { get; set; } = null!;
}