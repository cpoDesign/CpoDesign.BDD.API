Feature: Configuration
	In order to ensure the endpoint is working
	As application starts
	I *want* to ensure that the default values are populated in confiugration
	
@configuration
Scenario: The appsettings file contains correct configuration
	Given The application is using configuration key 'BaseUrl'
	When the application reads the configuration key
	Then the configuration value value should be 'https://localhost:44313/'
	And the configuration value should contain 'https'