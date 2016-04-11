package md5b5154ec1183559dade55f44b06fff203;


public class AuthenticationPageRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.PageRenderer
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ReactiveBrite.Droid.Renderers.AuthenticationPageRenderer, ReactiveBrite.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AuthenticationPageRenderer.class, __md_methods);
	}


	public AuthenticationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == AuthenticationPageRenderer.class)
			mono.android.TypeManager.Activate ("ReactiveBrite.Droid.Renderers.AuthenticationPageRenderer, ReactiveBrite.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AuthenticationPageRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == AuthenticationPageRenderer.class)
			mono.android.TypeManager.Activate ("ReactiveBrite.Droid.Renderers.AuthenticationPageRenderer, ReactiveBrite.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AuthenticationPageRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == AuthenticationPageRenderer.class)
			mono.android.TypeManager.Activate ("ReactiveBrite.Droid.Renderers.AuthenticationPageRenderer, ReactiveBrite.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
