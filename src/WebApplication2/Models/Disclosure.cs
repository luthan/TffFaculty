using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Disclosure
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public bool CommercialInterest { get; set; }

        public bool SalarySelf { get; set; }
        public bool SalarySpouse { get; set; }
        public string SalaryInfo { get; set; }

        public bool RoyaltySelf { get; set; }
        public bool RoyaltySpouse { get; set; }
        public string RoyaltyInfo { get; set; }

        public bool IpRightsPatentSelf { get; set; }
        public bool IpRightsPatentSpouse { get; set; }
        public string IpRightsPatentInfo { get; set; }

        public bool ConsultingSelf { get; set; }
        public bool ConsultingSpouse { get; set; }
        public string ConsultingInfo { get; set; }

        public bool SpeakersBureauSelf { get; set; }
        public bool SpeakersBureauSpouse { get; set; }
        public string SpeakersBureauInfo { get; set; }

        public bool ContractedResearchSelf { get; set; }
        public bool ContractedResearchSpouse { get; set; }
        public string ContractedResearchInfo { get; set; }

        public bool OwnershipSelf { get; set; }
        public bool OwnershipSpouse { get; set; }
        public string OwnershipInfo { get; set; }

        public bool OtherSelf { get; set; }
        public bool OtherSpouse { get; set; }
        public string OtherDescription { get; set; }
        public string OtherInfo { get; set; }

        public bool WillImpactPresentation { get; set; }

        public string UnapprovedDrugReference { get; set; }
        public string DrugList { get; set; }

        public bool DisclosedAllRelevantRelationships { get; set; }

        public bool SignatureConfirm { get; set; }
        public string SignatureText { get; set; }
        public DateTime SignatureDate { get; set; }
        public bool IsBackup { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}