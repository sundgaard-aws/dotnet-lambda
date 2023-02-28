// Decompiled with JetBrains decompiler
// Type: AthenaODBCFunction.Function
// Assembly: AthenaODBCFunction, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FBA2DA6-4FBE-4740-8ABD-9DA8A13207A4
// Assembly location: C:\github\dotnet-lambda-function\AthenaODBCFunction\src\AthenaODBCFunction\bin\AthenaODBCFunction.dll

using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AthenaODBCFunction
{
  public class Function
  {
    public async Task<List<Database>> FunctionHandler(
      InputData inputData,
      ILambdaContext context)
    {
      AmazonAthenaClient athenaClient = new AmazonAthenaClient();
      AmazonAthenaClient amazonAthenaClient1 = athenaClient;
      ListDatabasesRequest request1 = new ListDatabasesRequest();
      request1.CatalogName = "AwsDataCatalog";
      CancellationToken cancellationToken1 = new CancellationToken();
      ListDatabasesResponse result = await amazonAthenaClient1.ListDatabasesAsync(request1, cancellationToken1);
      AmazonAthenaClient amazonAthenaClient2 = athenaClient;
      StartQueryExecutionRequest request2 = new StartQueryExecutionRequest();
      request2.QueryString = "select * from vindb.cars";
      request2.ResultConfiguration = new ResultConfiguration()
      {
        OutputLocation = "s3://athena-query-bucket-s3"
      };
      CancellationToken cancellationToken2 = new CancellationToken();
      StartQueryExecutionResponse startQueryExecution = await amazonAthenaClient2.StartQueryExecutionAsync(request2, cancellationToken2);
      bool flag = false;
      for (int circuitBreaker = 0; !flag && circuitBreaker < 50; ++circuitBreaker)
      {
        Console.WriteLine(string.Format("circuitBreaker={0}", (object) circuitBreaker));
        AmazonAthenaClient amazonAthenaClient3 = athenaClient;
        GetQueryExecutionRequest request3 = new GetQueryExecutionRequest();
        request3.QueryExecutionId = startQueryExecution.QueryExecutionId;
        CancellationToken cancellationToken3 = new CancellationToken();
        GetQueryExecutionResponse queryExecutionAsync = await amazonAthenaClient3.GetQueryExecutionAsync(request3, cancellationToken3);
        string str = queryExecutionAsync.QueryExecution.Status.State.Value;
        flag = ConstantClass.op_Equality(str, (ConstantClass) QueryExecutionState.SUCCEEDED);
        if (ConstantClass.op_Equality(str, (ConstantClass) QueryExecutionState.FAILED))
          throw new Exception(queryExecutionAsync.QueryExecution.Status.StateChangeReason);
        if (ConstantClass.op_Equality(str, (ConstantClass) QueryExecutionState.RUNNING))
          Thread.Sleep(10);
      }
      AmazonAthenaClient amazonAthenaClient4 = athenaClient;
      GetQueryResultsRequest request4 = new GetQueryResultsRequest();
      request4.QueryExecutionId = startQueryExecution.QueryExecutionId;
      CancellationToken cancellationToken4 = new CancellationToken();
      GetQueryResultsResponse queryResultsAsync = await amazonAthenaClient4.GetQueryResultsAsync(request4, cancellationToken4);
      Console.WriteLine(string.Format("Result Count {0}", (object) queryResultsAsync.ResultSet.Rows.Count));
      Console.WriteLine("Result Count " + JsonConvert.SerializeObject((object) queryResultsAsync.ResultSet.Rows.Take<Row>(10)));
      List<Database> databaseList = result.DatabaseList;
      athenaClient = (AmazonAthenaClient) null;
      result = (ListDatabasesResponse) null;
      startQueryExecution = (StartQueryExecutionResponse) null;
      return databaseList;
    }
  }
}
