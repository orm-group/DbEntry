﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Lephone.Data;
using Lephone.Util;
using Mono.Cecil;

namespace Lephone.CodeGen.Processor
{
    public class AssemblyProcessor
    {

        private static readonly Dictionary<Type, MethodInfo> TypeDict;

        static AssemblyProcessor()
        {
            TypeDict = new Dictionary<Type, MethodInfo>();
            var types = new[] { typeof(Date), typeof(Time), typeof(DateTime), typeof(Guid), typeof(TimeSpan), typeof(decimal), typeof(string) };
            foreach (var type in types)
            {
                var mi = type.GetMethod("op_Inequality", ClassHelper.AllFlag);
                TypeDict.Add(type, mi);
            }
        }

        public void Process(string name)
        {
            var assembly = Assembly.LoadFrom(name);
            var models = DbEntry.GetAllModels(assembly);

            var module = ModuleDefinition.ReadModule(name);
            foreach (var model in models)
            {
                var type = module.GetType(model.FullName);
                var processor = new ModelProcessor(type);
                processor.Process();
            }

            module.Write(name + ".dll");

            Console.ReadLine();
        }

    }
}