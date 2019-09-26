#!/bin/bash
echo "export const environment = { production: false, apibaseurl: '${MYAPI_BASEURL}'} " >> /app/ui/src/enviornments/environment.ts
