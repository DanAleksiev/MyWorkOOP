using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
    {
    public class Controller : IController
        {
        private StudentRepository students;
        private UniversityRepository universitys;
        private SubjectRepository subjects;
        private string[] avaibleCategories = { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };

        public Controller()
            {
            this.students = new StudentRepository();
            this.universitys = new UniversityRepository();
            this.subjects = new SubjectRepository();
            }

        public string AddSubject(string subjectName, string subjectType)
            {
            if (!avaibleCategories.Contains(subjectType))
                {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
                }
            if (subjects.FindByName(subjectName) != null)
                {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
                }

            ISubject subject = null;

            if (subjectType == nameof(EconomicalSubject))
                {
                subject = new EconomicalSubject(subjects.Models.Count + 1, subjectName);
                }
            else if (subjectType == nameof(HumanitySubject))
                {
                subject = new HumanitySubject(subjects.Models.Count + 1, subjectName);
                }
            else if (subjectType == nameof(TechnicalSubject))
                {
                subject = new TechnicalSubject(subjects.Models.Count + 1, subjectName);
                }

            subjects.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
            }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
            {
            if (universitys.FindByName(universityName) != null)
                {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
                }
            List<int> subjectId = new List<int>();
            foreach (var subject in requiredSubjects)
                {
                subjectId.Add(subjects.FindByName(subject).Id);
                }
            IUniversity uni = new University(universitys.Models.Count + 1, universityName, category, capacity, subjectId);
            universitys.AddModel(uni);
            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universitys.GetType().Name);
            }

        public string AddStudent(string firstName, string lastName)
            {

            if (students.FindByName($"{firstName} {lastName}") != null)
                {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
                }
            IStudent student = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
            }
        public string TakeExam(int studentId, int subjectId)
            {
            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student == null)
                {
                return string.Format(OutputMessages.InvalidStudentId);
                }
            if (subject == null)
                {
                return string.Format(OutputMessages.InvalidSubjectId);
                }
            if (student.CoveredExams.FirstOrDefault(x => x == subject.Id) != default)
                {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
                }

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
            }

        public string ApplyToUniversity(string studentName, string universityName)
            {
            IStudent student = students.FindByName(studentName);
            IUniversity university = universitys.FindByName(universityName);

            if (student == null)
                {
                return string.Format(OutputMessages.StudentNotRegitered, student.FirstName, student.LastName);
                }
            if (university == null)
                {
                return string.Format(OutputMessages.UniversityNotRegitered, university.Name);
                }

            List<int> covered = student.CoveredExams.ToList();
            List<int> uniReq = university.RequiredSubjects.ToList();
            foreach(int i in uniReq)
                {
                if (!covered.Contains(i))
                    {
                    return string.Format(OutputMessages.StudentHasToCoverExams,studentName,universityName);                    
                    }
                }

            if (student.University == university)
                {
                return string.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, student.University.Name);
                }

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, university.Name);
            }


        public string UniversityReport(int universityId)
            {
            IUniversity university = universitys.FindById(universityId);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            int studentsCount = students.Models.Count(x => x.University == university);
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsCount}");
            return sb.ToString().Trim();
            }
        }
    }