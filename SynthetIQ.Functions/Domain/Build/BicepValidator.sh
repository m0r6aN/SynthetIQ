vali#!/bin/bash

# check to make sure all endpoints have a .bicep, XML, and main.bicep file
endpoints=`ls *endpoint*`

for endpoint in $endpoints
do
  # check for .bicep
  if [ ! -f $endpoint.bicep ]; then
    echo "Missing .bicep for $endpoint"
    exit 1
  fi

  # check for XML
  if [ ! -f $endpoint.xml ]; then
    echo "Missing XML for $endpoint"
    exit 1
  fi

  # check for main.bicep
  if [ ! `grep -Fxq $endpoint main.bicep` ]; then
    echo "Missing entry in main.bicep for $endpoint"
    exit 1
  fi
done

echo "All endpoints have corresponding .bicep, XML, and main.bicep files"
exit 0