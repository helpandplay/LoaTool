﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaTool.Define.Interfaces;
public interface IDialog //Window Interface
{
    object DataContext { get; set; }
    void Show();
    void Close();
    bool? ShowDialog();
    bool Focus();
}
