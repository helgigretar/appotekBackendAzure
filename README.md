# appotekBackendAzure
## Routes hvað þau fá og hverju þau skila
## POST
#### /api/login("username","password") --> Status hvort þú ert loggaður inn eða ekki
#### /api/register("username","password","repeatpassword","name") --> Status hvort þú náður að búa til aðgang annars villur
## GET
#### /api/getAllMeds() --> Skilar top 10 lyfjum
#### /api/getByVnr/num --> num er vnr og þetta skilar lyfinu sem var skannað
