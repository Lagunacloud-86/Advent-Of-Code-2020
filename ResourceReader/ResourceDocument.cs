using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceReader
{
    public class ResourceDocument
    {
        //private const Int32 ReadSize = 4096;
        private readonly Char[] _document;


        private ResourceDocument(in Char[] document)
        {
            _document = new Char[document.Length];
            Array.Copy(document, _document, document.Length);

        }


        public ref readonly Char[] ToCharArray()
        {
            return ref _document;
        }


        public static async ValueTask<ResourceDocument> DocumentFromEmbeddedResource(
            Assembly assembly, 
            String embeddedResource,
            CancellationToken cancellationToken = default)
        {
            Char[] document = null;
            using (Stream stream = assembly.GetManifestResourceStream(embeddedResource))
            {
                if (stream == null)
                    return default;
                document = new char[stream.Length];
                using (StreamReader reader = new StreamReader(stream))
                {
                    Memory<Char> memory = new Memory<Char>(document);
                    Int32 readCount = 0;
                    do
                    {
                        readCount = await reader
                            .ReadBlockAsync(memory, cancellationToken)
                            .ConfigureAwait(false);
                    } while (readCount > 0);
                }
            }

            return new ResourceDocument(document);
        }
    }
}
