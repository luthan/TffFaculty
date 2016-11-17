using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class DisclosureViewModel
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        [Display(Name = "Within the past 12 months, have you or your spouse/life partner received support from, or had a financial relationship with, a commercial interest?")]
        [Required(ErrorMessage = "This is required")]
        public string CommercialInterest { get; set; }


        public bool SalarySelf { get; set; }
        public bool SalarySpouse { get; set; }
        [RequiredIf("SalarySelf == true || SalarySpouse == true", ErrorMessage = "This field is required")]
        public string SalaryInfo { get; set; }

        public bool RoyaltySelf { get; set; }
        public bool RoyaltySpouse { get; set; }
        [RequiredIf("RoyaltySelf == true || RoyaltySpouse == true", ErrorMessage = "This field is required")]
        public string RoyaltyInfo { get; set; }

        public bool IpRightsPatentSelf { get; set; }
        public bool IpRightsPatentSpouse { get; set; }
        [RequiredIf("IpRightsPatentSelf == true || IpRightsPatentSpouse == true", ErrorMessage = "This field is required")]
        public string IpRightsPatentInfo { get; set; }

        public bool ConsultingSelf { get; set; }
        public bool ConsultingSpouse { get; set; }
        [RequiredIf("ConsultingSelf == true || ConsultingSpouse == true", ErrorMessage = "This field is required")]
        public string ConsultingInfo { get; set; }

        public bool SpeakersBureauSelf { get; set; }
        public bool SpeakersBureauSpouse { get; set; }
        [RequiredIf("SpeakersBureauSelf == true || SpeakersBureauSpouse == true", ErrorMessage = "This field is required")]
        public string SpeakersBureauInfo { get; set; }

        public bool ContractedResearchSelf { get; set; }
        public bool ContractedResearchSpouse { get; set; }
        [RequiredIf("ContractedResearchSelf == true || ContractedResearchSpouse == true", ErrorMessage = "This field is required")]
        public string ContractedResearchInfo { get; set; }

        public bool OwnershipSelf { get; set; }
        public bool OwnershipSpouse { get; set; }
        [RequiredIf("OwnershipSelf == true || OwnershipSpouse == true", ErrorMessage = "This field is required")]
        public string OwnershipInfo { get; set; }

        public bool OtherSelf { get; set; }
        public bool OtherSpouse { get; set; }
        [RequiredIf("OtherSelf == true || OtherSpouse == true", ErrorMessage = "This field is required")]
        public string OtherDescription { get; set; }
        [RequiredIf("OtherSelf == true || OtherSpouse == true", ErrorMessage = "This field is required")]
        public string OtherInfo { get; set; }

        [AssertThat("DisclosedAllRelevantRelationships == 'Yes'", ErrorMessage = "You are required to agree")]
        [Display(Name = "I have disclosed all relevant financial relationships")]
        public string DisclosedAllRelevantRelationships { get; set; }

        [Display(Name = "If you reported relationships in the chart above, will any of these relationships impact your ability to present a balanced and unbiased presentation?")]
        [AssertThat("WillImpactPresentation == 'No'", ErrorMessage = "You are required to answer with 'No'")]
        public string WillImpactPresentation { get; set; }

        [Display(Name = "Do you intend to reference unlabeled/unapproved uses of drugs or products in your presentation?")]
        [Required(ErrorMessage = "This is required")]
        public string UnapprovedDrugReference { get; set; }

        [RequiredIf("UnapprovedDrugReference == 'Yes'", ErrorMessage = "This field is required")]
        [Display(Name = "Please list the names of devices or drugs you will reference and unlabeled/unapproved use:")]
        public string DrugList { get; set; }

        [Display(Name = "My typed full name below indicates that I have read and completed this form myself and, to the best of my ability, provided current and accurate information. I am aware that the financial disclosure information provided in this form will be shared with learners prior to their engagement in this CE activity.")]
        [AssertThat("SignatureConfirm == true", ErrorMessage = "Confirmation is required!")]
        public bool SignatureConfirm { get; set; }

        [Display(Name = "Signature")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string SignatureText { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SignatureDate { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<string> FacultyRoles { get; set; }
    }
}