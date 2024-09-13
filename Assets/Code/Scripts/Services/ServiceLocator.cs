namespace Code.Scripts.Services {
	public static class ServiceLocator<T> where T : class, new () {
		public static T Service { get; set; }
	}
}