﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using MyControl.view;

namespace MyControl.model
{
    class GlobalVars
    {
        public static MainWindow mainWindow;
        public static RotinaPage2_Alocar_Debitos rotinaPage2;
        public static FirestoreDb db;
    }
}
