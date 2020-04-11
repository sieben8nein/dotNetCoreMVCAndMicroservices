FROM mongo

COPY users/init.json /init.json
CMD ["mongoimport --host mongodb --db reach-engine --collection Users --type json --file /users/init.json --jsonArray"] 
	#"; mongoimport --host mongodb --db reach-engine --collection Users --type json --file /users/init.json --jsonArray"]