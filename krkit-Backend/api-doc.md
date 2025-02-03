{
  "openapi": "3.0.1",
  "info": {
    "title": "krkit-Backend",
    "version": "1.0"
  },
  "paths": {
    "/api/Auth/register": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Auth/login": {
      "post": {
        "tags": [
          "Auth"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/LoginRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Product": {
      "get": {
        "tags": [
          "Product"
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Product/add": {
      "post": {
        "tags": [
          "Product"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/AddProductRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/AddProductRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/AddProductRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Product/select": {
      "get": {
        "tags": [
          "Product"
        ],
        "parameters": [
          {
            "name": "barcode",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Product/update": {
      "put": {
        "tags": [
          "Product"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateProductRequestDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateProductRequestDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateProductRequestDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/Product/delete/{id}": {
      "delete": {
        "tags": [
          "Product"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/ToDo": {
      "get": {
        "tags": [
          "ToDo"
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/ToDo/{id}": {
      "get": {
        "tags": [
          "ToDo"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/ToDo/add": {
      "post": {
        "tags": [
          "ToDo"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/AddToDoItemDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/AddToDoItemDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/AddToDoItemDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/ToDo/update/{id}": {
      "put": {
        "tags": [
          "ToDo"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateToDoItemDto"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateToDoItemDto"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/UpdateToDoItemDto"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    },
    "/api/ToDo/delete/{id}": {
      "delete": {
        "tags": [
          "ToDo"
        ],
        "parameters": [
          {
            "name": "id",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "AddProductRequestDto": {
        "type": "object",
        "properties": {
          "companyName": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "price": {
            "type": "number",
            "format": "double"
          },
          "quantity": {
            "type": "integer",
            "format": "int32"
          },
          "barcode": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "AddToDoItemDto": {
        "type": "object",
        "properties": {
          "title": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "LoginRequestDto": {
        "type": "object",
        "properties": {
          "username": {
            "type": "string",
            "nullable": true
          },
          "password": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "UpdateProductRequestDto": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "companyName": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "price": {
            "type": "number",
            "format": "double"
          },
          "quantity": {
            "type": "integer",
            "format": "int32"
          },
          "barcode": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "UpdateToDoItemDto": {
        "type": "object",
        "properties": {
          "title": {
            "type": "string",
            "nullable": true
          },
          "description": {
            "type": "string",
            "nullable": true
          },
          "isCompleted": {
            "type": "boolean"
          }
        },
        "additionalProperties": false
      }
    },
    "securitySchemes": {
      "Bearer": {
        "type": "http",
        "description": "Bearer YAZMA Direk YAZ",
        "scheme": "Bearer",
        "bearerFormat": "JWT"
      }
    }
  },
  "security": [
    {
      "Bearer": [ ]
    }
  ]
}