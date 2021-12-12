using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smart_dorms.Utils
{ 
    public enum UserRole
    {
        Student = 1,
        Administrator = 2,
        TBA = 3
    }

    public enum StudentRequestType
    {
        ObiectDefect = 1,
        SchimbareObiect = 2,
        AdaugareObiect = 3,
        EliminareObiect = 4,
        Dezinsectie = 5,
        Asistenta = 6,
        Student = 7,
        SchimbareCamera = 8
    }

    public enum RequestStatus
    {
        Pending = 1,
        Completed = 2,
        Allocated = 3,
        NeedsTime = 4,
        Rejected = 5,
        Invalid = 6
    }
}