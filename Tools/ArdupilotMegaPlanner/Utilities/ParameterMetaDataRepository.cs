﻿using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;

namespace ArdupilotMega.Utilities
{
   public class ParameterMetaDataRepository
   {
      private static XDocument _parameterMetaDataXML;

      /// <summary>
      /// Initializes a new instance of the <see cref="ParameterMetaDataRepository"/> class.
      /// </summary>
      public ParameterMetaDataRepository()
      {
         string paramMetaDataXMLFileName = String.Format("{0}\\{1}", Application.StartupPath, ConfigurationManager.AppSettings["ParameterMetaDataXMLFileName"]);
         if (File.Exists(paramMetaDataXMLFileName))
            _parameterMetaDataXML = XDocument.Load(paramMetaDataXMLFileName);
      }

      /// <summary>
      /// Gets the parameter meta data.
      /// </summary>
      /// <param name="nodeKey">The node key.</param>
      /// <param name="metaKey">The meta key.</param>
      /// <returns></returns>
      public string GetParameterMetaData(string nodeKey, string metaKey)
      {
         if(_parameterMetaDataXML != null)
         {
            // Use this to find the endpoint node we are looking for
            // Either it will be pulled from a file in the ArduPlane hierarchy or the ArduCopter hierarchy
            var element = _parameterMetaDataXML.Element("Params").Element(MainV2.cs.firmware.ToString());
            if(element != null && element.HasElements)
            {
               var node = element.Element(nodeKey);
               if(node != null && node.HasElements)
               {
                  var metaValue = node.Element(metaKey);
                  if(metaValue != null)
                  {
                     return metaValue.Value;
                  }
               }
            }
         }
         return string.Empty;
      }
   }
}
