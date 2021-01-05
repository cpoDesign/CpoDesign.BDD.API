Feature: Healthcheck
	In order to ensure the endpoint is working
	As a system verification
	I *want* to be told if application is up
	
@healthcheck
Scenario: Healthcheck returns a healthy language
	Given A call to the endpoint using template file 'healthcheck.postman.json'
	When make request using the template
	Then result should contain the word 'Healthy'