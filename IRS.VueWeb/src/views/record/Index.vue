<template>
  <div id="admin">
    <el-form id="toolbar" :inline="true" :model="searchForm" class="demo-form-inline">
      <el-form-item label="姓名">
        <el-input v-model="searchForm.RecordUser" placeholder="用户姓名"></el-input>
      </el-form-item>
      <el-form-item label="宿舍号">
        <el-input v-model="searchForm.UserDorm" placeholder="宿舍号"></el-input>
      </el-form-item>
      <el-form-item label="报修时间">
        <el-date-picker v-model="searchForm.RecordTime" type="date" value-format="yyyy-MM-dd" placeholder="选择日期"></el-date-picker>
      </el-form-item>
      <el-form-item>
        <el-button @click="find()" plain>查询</el-button>
        <el-button type="info" @click="findReset()" plain>重置</el-button>
      </el-form-item>
    </el-form>
    <div id="datagrid">
      <div class="toolbar">
        <el-button plain icon="el-icon-plus" @click="add()">添加</el-button>
        <!-- <el-button plain icon="el-icon-download" @click="download()">导出</el-button> -->
      </div>

      <el-table :data="tableData" :border="true" style="width: 100%" @select-all="selectChange" @selection-change="selectChange" v-loading="loading" @expand-change="expandChange">
        <el-table-column type="expand">
          <el-table :border="true" :data="processData" style="width: 100%" v-loading="loading">
            <el-table-column prop="processingRecordId" label="序号" width="70"></el-table-column>
            <el-table-column prop="processingPeople" label="维修员" width="80"></el-table-column>
            <el-table-column prop="processingTime" label="维修时间" width="200"></el-table-column>
            <el-table-column prop="processingDesc" label="维修描述" width="250"></el-table-column>
            <el-table-column prop="processingResult" v-if="false" label="维修结果"></el-table-column>
            <el-table-column prop="processingResultName" label="维修结果"></el-table-column>
          </el-table>
        </el-table-column>
        <el-table-column prop="recordId" label="序号" v-if="false" width="70">
        </el-table-column>
        <el-table-column prop="recordUser" label="报修人" width="80">
        </el-table-column>
        <el-table-column prop="userPhone" label="联系电话" width="150">
        </el-table-column>
        <el-table-column prop="dormCategoryName" label="宿舍楼" width="80" :filters=this.dormfilter :filter-method="filterAddress" filter-placement="bottom-end">
        </el-table-column>
        <el-table-column prop="dormCategory" label="宿舍楼Id" v-if="false" width="80">
        </el-table-column>
        <el-table-column prop="userDorm" label="宿舍号" width="80">
        </el-table-column>
        <el-table-column prop="faultCategoryName" label="故障分类" width="100" :filters=this.faultfilter :filter-method="filterFault" filter-placement="bottom-end">
        </el-table-column>
        <el-table-column prop="faultCategory" label="故障分类Id" v-if="false" width="100">
        </el-table-column>
        <el-table-column prop="faultDesc" label="故障描述" width="100">
        </el-table-column>
        <el-table-column prop="recordTime" label="报修时间" width="100">
        </el-table-column>
        <el-table-column prop="faultResultName" label="处理情况" width="100" :filters=this.resultfilter :filter-method="filterResult" filter-placement="bottom-end">
        </el-table-column>
        <el-table-column prop="faultResult" label="处理情况Id" v-if="false" width="100">
        </el-table-column>

        <el-table-column label="操作">
          <template slot-scope="scope">
            <div v-if="scope.row.faultResult != endFlag ">
              <el-tooltip content="编辑" placement="right-end">
                <el-button size="small" plain icon="el-icon-edit-outline" @click="edit(scope.row)"></el-button>
              </el-tooltip>
              <el-tooltip content="删除" placement="right-end">
                <el-button plain icon="el-icon-delete" type="danger" size="small" @click="del(scope.row)"></el-button>
              </el-tooltip>
              <el-tooltip content="新增维修记录" placement="right-end">
                <el-button plain icon="el-icon-edit" type="primary" size="small" @click="addprocess(scope.row)"></el-button>
              </el-tooltip>
            </div>
          </template>
        </el-table-column>
      </el-table>

      <el-dialog title="报修信息" :visible.sync="editDialogFormVisible" @close="cancel(); ">
        <el-form ref="form" :model="form" label-width="80px" label-position="top">
          <el-row class="first-row">
            <el-col :span="10" class="first-column" :offset="2">
              <el-form-item label="报修人">
                <el-input v-model="form.recordUser" placeholder=''></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="联系电话">
                <el-input v-model="form.userPhone" placeholder=''></el-input>
              </el-form-item>
            </el-col>
          </el-row>

          <el-row class="normal-row">
            <el-col :span="10" class="first-column" :offset="2">
              <el-form-item label="宿舍楼">
                <template>
                  <el-select v-model="form.dormCategory" placeholder="请选择">
                    <el-option v-for="item in dormList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                      <span style="float: left">{{ item.categoryInfoName }}</span>
                    </el-option>
                  </el-select>
                </template>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="宿舍号">
                <el-input v-model="form.userDorm"></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="normal-row" v-if="isEdit">
            <el-col :span="10" class="first-column" :offset="2">
              <el-form-item label="处理情况">
                <template>
                  <el-select v-model="form.faultResult" placeholder="请选择">
                    <el-option v-for="item in resultList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                      <span style="float: left">{{ item.categoryInfoName }}</span>
                    </el-option>
                  </el-select>
                </template>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="报修时间">
                <el-date-picker v-model="form.recordTime" type="datetime" value-format="yyyy-MM-dd HH:mm:ss" placeholder="选择日期"></el-date-picker>
              </el-form-item>
            </el-col>
          </el-row>

          <el-row class="last-row" style="height: 200px; overflow: hidden">
            <el-col :span="10" class="first-column" :offset="2" style="height: 100%">
              <el-form-item label="故障分类">
                <template>
                  <el-select v-model="form.faultCategory" placeholder="请选择">
                    <el-option v-for="item in faultList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                      <span style="float: left">{{ item.categoryInfoName }}</span>
                    </el-option>
                  </el-select>
                </template>
              </el-form-item>
            </el-col>
            <el-col :span="10" style="height: 100%">
              <el-form-item label="故障描述">
                <el-input type="textarea" :autosize="{ minRows: 2, maxRows: 4}" v-model="form.faultDesc"></el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="cancel()">取 消</el-button>
          <el-button type="primary" @click="save()">确 定</el-button>
        </div>
      </el-dialog>

      <el-dialog title="处理信息" :visible.sync="addDialogFormVisible" @close="addcancel(); ">
        <el-form ref="processForm" :model="processForm" label-width="80px" label-position="top">
          <el-row class="first-row">
            <el-col :span="10" class="first-column" :offset="2">
              <el-form-item label="处理人">
                <el-input v-model="processForm.processingPeople" placeholder=''></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="处理情况">
                <template>
                  <el-select v-model="processForm.processingResult" placeholder="请选择">
                    <el-option v-for="item in resultList" :key="item.categoryInfoId" :label="item.categoryInfoName" :value="item.categoryInfoId">
                      <span style="float: left">{{ item.categoryInfoName }}</span>
                    </el-option>
                  </el-select>
                </template>
              </el-form-item>
            </el-col>
          </el-row>

          <el-row class="last-row" style="height: 200px; overflow: hidden">
            <el-col :span="10" class="first-column" :offset="2" style="height: 100%">
              <el-form-item label="处理时间">
                <el-date-picker v-model="processForm.processingTime" type="datetime" value-format="yyyy-MM-dd HH:mm:ss" placeholder="选择日期"></el-date-picker>
              </el-form-item>
            </el-col>
            <el-col :span="10" style="height: 100%">
              <el-form-item label="处理描述">
                <el-input type="textarea" :autosize="{ minRows: 2, maxRows: 4}" v-model="processForm.processingDesc"></el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="addcancel()">取 消</el-button>
          <el-button type="primary" @click="saveprocess()">确 定</el-button>
        </div>
      </el-dialog>

      <el-row class="page">
        <el-col :span="20">
          <el-pagination background @current-change="pagination" @size-change="sizeChange" :current-page.sync="current_page" :page-sizes="[10,20,25,50]" layout="total,sizes,prev, pager, next" :page-size.sync="pageSize" :total="total">
          </el-pagination>
        </el-col>
      </el-row>
    </div>

    <download-xls :show="isShowDownload" :template-file="downloadFile" module="admin" :pageSize="pageSize" :page="current_page" :search="searchForm" @close-download="closeDownload"></download-xls>
  </div>
</template>

<script>
import { getToken } from "@/utils/auth";
import {
  getInfo,
  getInfoById,
  updateInfo,
  addInfo,
  deleteInfoById,
  getCurrentPage,
  getMenuByType,
  getEndFlagByType,
  SearchModel,
  getRecordById,
  addProcessInfo,
  Model,
  ProcessModel
} from "@/api/record";

import {config} from "./../../config/index";
import DownloadXls from "@/views/components/DownloadXls";
import {Tools} from "@/views/utils/Tools";

export default {
  components: {
    DownloadXls
  },
  data() {
    return {
      searchForm: new SearchModel(),
      form: new Model(),
      processForm: new ProcessModel(),
      imageUrl: "",
      tableData: [],
      processData:[],
      resultList:[],
      resultfilter:[],
      faultList:[],
      faultfilter:[],
      dormList:[],
      dormfilter:[],
      editDialogFormVisible: false,
      addDialogFormVisible: false,
      downloadFile: config.site + '/xls/报修管理.xls',
      uploadId: "",
      endFlag:0,
      isNew: false,
      isEdit: false,
      isShowDownload: false,
      loading: false,
      current_page: 1,
      total: 0,
      pageSize: 10,
    };
  },
  methods: {
    find(){
       this.fetchData()
    },
    findReset() {
       this.searchForm = new SearchModel()
       this.fetchData()
    },
    add(){
       this.isNew = true
       this.form = new Model()
       this.editDialogFormVisible = true
    },
    download() {
      this.isShowDownload = true
    },
    closeDownload() {
      this.isShowDownload = false
    },
    fetchData(searchObj = this.searchForm , page = this.current_page, pageSize = this.pageSize) {
       this.loading = true
       getInfo(searchObj, page, pageSize).then(response => {
          let result = response.data;
          this.tableData = result;
          this.total = response.total;
          this.loading = false;
        })
        .catch(() => {
           this.loading = false
        });
    },
    addprocess(row) {
      let id = row.recordId;
      this.processForm = new ProcessModel();
      this.processForm.recordId = id;
      this.addDialogFormVisible = true;
    },
    edit(row) {
      let id = row.recordId;
      this.uploadId = id;
      getInfoById(id).then(response => {
        let result = response.data;
        this.form = result;
        this.isEdit = true;
        this.editDialogFormVisible = true;
      });
    },
    cancel() {
       this.isNew = false
       this.isEdit = false
       this.editDialogFormVisible = false;
    },
    addcancel() {
       this.addDialogFormVisible = false;
    },
    save() {
      this.editDialogFormVisible = false;
      if (this.isNew) {
        this.isNew = false
        this.newData()
      }
      if (this.isEdit) {
        this.isEdit = false
        this.updateData()
      }
    },
    saveprocess() {
      this.addDialogFormVisible = false;
      addProcessInfo(this.processForm).then(response => {
           Tools.success(this, '处理记录添加成功')
           this.fetchData();
      }).catch(err => {
         Tools.error(this, err.response.data)
      })
    },
    newData() {
      addInfo(this.form).then(response => {
           Tools.success(this, '报修记录添加成功')
           this.fetchData();
      }).catch(err => {
         Tools.error(this, err.response.data)
      })
    },
    updateData() {
      updateInfo(this.uploadId, this.form)
        .then(response => {
          //成功执行内容
          let result = response.status_code;
          if (result == 200) {
            let currentId = this.form.recordId;
            let record = 0;
            record = this.tableData.findIndex((val, index) => {
              return val.recordId == currentId;
            });
            this.tableData.splice(record, 1, this.form);
            this.editDialogFormVisible = false;
            Tools.success(this,'报修信息修改成功')
            this.fetchData();
          }
        })
        .catch((err) => {
          //失败执行内容
          Tools.error(this, err.response.data)
        });
    },
    del(row) {
      this.$confirm("此操作将永久删除该报修记录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          let id = row.recordId;
          let record = null;
          record = this.tableData.findIndex(val => val.recordId == id);

          deleteInfoById(id)
            .then(response => {
              let result = response.status_code;
              if (result == 200) {
                if (response.message) {
                Tools.success(this, response.message)
                } else {
                Tools.success(this, "删除成功!")
                this.tableData.splice(record, 1);
                }
              }
            })
            .catch(() => {
              //失败执行内容
            });
        })
        .catch(() => {});
    },
    expandChange(row, expandedRows){
      getRecordById(row.recordId).then(res =>{
        let result = res.data
        this.processData = result
      })
    },
    selectChange(selection) {
       this.multiSelect = selection;
    },
    // 分页功能
    pagination(val){
      this.current_page = val;
      this.fetchData();
    },
    sizeChange(val){
      this.pageSize = val
      this.fetchData();
    },
    filterAddress(value, row) {
      return row.dormCategory === value;
    },
    filterResult(value, row) {
      return row.faultResult === value;
    },
    filterFault(value, row) {
      return row.faultCategory === value;
    }
  },
  beforeCreate() {
    getMenuByType(2).then(res =>{
      this.faultList = res.data
      for(var i = 0; i < this.faultList.length; i++) {
          var obj = {text:this.faultList[i].categoryInfoName,value:this.faultList[i].categoryInfoId}
          this.faultfilter.push(obj);
        };
    })
    getMenuByType(1).then(res => {
      this.dormList = res.data
      for(var i = 0; i < this.dormList.length; i++) {
          var obj = {text:this.dormList[i].categoryInfoName,value:this.dormList[i].categoryInfoId}
          this.dormfilter.push(obj);
        };
    })
    getEndFlagByType(3).then(res => {
      let id = res.data;
      this.endFlag = id;
    })
    getMenuByType(3).then(res =>{
      this.resultList = res.data
      for(var i = 0; i < this.resultList.length; i++) {
          var obj = {text:this.resultList[i].categoryInfoName,value:this.resultList[i].categoryInfoId}
          this.resultfilter.push(obj);
        };
      this.fetchData()
    }).catch(err => {
    })
  }
};
</script>

<style lang="scss">
@import "./../../styles/app-main.scss";

.avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
}

.avatar-uploader .el-upload:hover {
    border-color: #20a0ff;
}

.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    line-height: 178px;
    text-align: center;
}
.avatar {
    width: 178px;
    height: 178px;
    display: block;
}

#admin .el-form--label-top .el-form-item__label {
    width: 100%;
    text-align: center;
}
#admin .el-form-item__content {
    text-align: center;
}
#admin .el-col-10 > .el-form-item > .el-form-item__content > .el-input {
    width: 80%;
}

#admin .first-row .el-col {
    border: 1px solid $border-color;
    border-left: 0px;
}
#admin .first-row .first-column {
    border-left: 1px solid $border-color;
}
#admin .normal-row .el-col {
    border: 1px solid $border-color;
    border-left: 0px;
    border-top: 0px;
}
#admin .normal-row .first-column {
    border-left: 1px solid $border-color;
}

#admin .last-row .el-col {
    border: 1px solid $border-color;
    border-top: 0px;
    border-left: 0px;
}
#admin .last-row .first-column {
    border-left: 1px solid $border-color;
}
#admin .last-row .first-column .el-upload-dragger {
    width: auto;
    height: auto;
}
</style>
