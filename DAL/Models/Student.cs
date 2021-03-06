﻿using DAL.Models.Enums;
using DAL.Models.Fees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Student : Base
    {
        /// <summary>
        /// Creates a new student and links their identity account with the school's
        /// student information.
        /// </summary>
        /// <param name="identityId">Unique identifier used to create an identity
        /// account for this student.
        /// </param>
        public Student(Guid identityId)
        {
            this.AccountId = identityId;
        }

        public Student()
        {

        }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string RegNo { get; set; }
        [Required]
        [MaxLength(4)]
        public int YearOfStudy { get; set; }
        [Required]
        [MaxLength(10)]
        public string AcademicYear { get; set; }
        [DefaultValue(0)]
        public SelectionType Selection { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentUnit> StudentUnits { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<FeePayment> Payments { get; set; }
    }
}
