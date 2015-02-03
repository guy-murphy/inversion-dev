using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetPostedFile: PostedFile, IRequestFile {

		//private readonly HttpPostedFile _underlyingFile;

		public AspNetPostedFile(HttpPostedFile file) : base(file.FileName, file.ContentType, file.InputStream) {
			//_underlyingFile = file;
		}

	}
}
